using FoldingAtomata.Modeling;
using FoldingAtomata.Modeling.DataBuffers;
using FoldingAtomata.Modeling.NMesh;
using FoldingAtomata.NTrajectory;
using FoldingAtomata.World;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.NViewer
{
    public class SlotViewer
    {
        public const float ATOM_SCALE = 0.15F;
        public const float BOND_SCALE = 0.06F;
        public const float ATOM_LIGHT_POWER = 2.0F;
        public const float PI = 3.141592653589F;
        public const int ANIMATION_SPEED = 2000;
        public int ATOM_STACKS { get; private set; }
        public int ATOM_SLICES { get; private set; }
        public Vector3 ATOM_LIGHT_POSITION
        {
            get
            {
                return new Vector3(-1.5F);
            }
        }
        public Vector3 ATOM_LIGHT_COLOR
        {
            get
            {
                return new Vector3(4);
            }
        }
        public Vector3 BOND_COLOR
        {
            get
            {
                return new Vector3(0.8F, 0.12F, 0.5F);
            }
        }

        public SlotViewer(Trajectory trajectory, Scene scene)
        {
            ATOM_STACKS = (Options.GetInstance().AtomStacks);
            ATOM_SLICES = (Options.GetInstance().AtomSlices);
            _scene = scene;
            _trajectory = trajectory;
            _snapshotIndexA = 0; 
            _snapshotIndexB = 1;

            Console.WriteLine();

            if (_trajectory.CountSnapshots() == 0)
                throw new Exception("No snapshots to work with!");
            else
                Console.WriteLine("Creating viewer for trajectory with {0} snapshots...", _trajectory.CountSnapshots());

            var RENDER_MODE = Options.GetInstance().RenderMode;
            if (RENDER_MODE == RenderMode.BALL_N_STICK)
            {
                AddAllAtoms();
                Console.WriteLine();
            }

            AddAllBonds();
            Console.WriteLine();

            Console.WriteLine("... done creating SlotViewer.");
        }
        public bool Animate(int deltaTime)
        {
            return false;
            /*if (_trajectory.CountSnapshots() <= 1)
                return false; //can't animate with one snapshot

            if (_atomInstances.Count == 0 && _bondInstance.GetInstanceCount() == 0)
                return false; //we have nothing to animate

            int b = UpdateSnapshotIndexes(deltaTime);
            var newPositions = AnimateAtoms(b);
            AnimateBonds(newPositions);

            return true;*/
        }
        public int UpdateSnapshotIndexes(int deltaTime)
        {
            _transitionTime += deltaTime;

            int a = _transitionTime / ANIMATION_SPEED; //int division on purpose
            int b = _transitionTime % ANIMATION_SPEED;
            _transitionTime = b;

            int snapshotCount = _trajectory.CountSnapshots();
            if (snapshotCount > 2 && a > 0)
            {
                if (Options.GetInstance().CycleSnapshots)
                { //FAHViewer-like bouncing animation
                    if (_snapshotIndexA < _snapshotIndexB)
                    { //going forward
                        _snapshotIndexA = _snapshotIndexA + a;
                        _snapshotIndexB = _snapshotIndexA + 1;
                        if (_snapshotIndexB == snapshotCount)
                            _snapshotIndexB -= 2;
                    }
                    else
                    { //going backwards
                        _snapshotIndexA = _snapshotIndexA - a;
                        _snapshotIndexB = _snapshotIndexA - 1;
                        if (_snapshotIndexB == -1)
                            _snapshotIndexB += 2;
                    }
                }
                else
                { //default jump-to-first-snapshot animation
                    _snapshotIndexA = (_snapshotIndexA + a) % (snapshotCount - 1);
                    _snapshotIndexB = _snapshotIndexA + 1;
                }
            }

            return b;
        }
        public List<Vector3> AnimateAtoms(int b)
        {
            var snapA = _trajectory.GetSnapshot(_snapshotIndexA);
            var snapB = _trajectory.GetSnapshot(_snapshotIndexB);

            var atoms = _trajectory.GetTopology().GetAtoms();
            List<Vector3> newPositions = new List<Vector3>();
            newPositions.Capacity = atoms.Count;

            for (int j = 0; j < atoms.Count; j++)
            {
                var startPosition = snapA.GetPosition(j);
                var endPosition = snapB.GetPosition(j);
                var position = (endPosition - startPosition) *
                                           (b / (float)ANIMATION_SPEED) + startPosition;

                newPositions.Add(position);
                ElementIndex index = _elementIndexes[j];
                if (_atomInstances.Count > 0)
                {
                    _atomInstances[index.elementIndex].SetModelMatrix(
                        index.instanceIndex, GenerateAtomMatrix(position, atoms[j]));
                }
            }

            return newPositions;
        }
        public void AnimateBonds(List<Vector3> atomPositions)
        {
            var bonds = _trajectory.GetTopology().GetBonds();
            for (int j = 0; j < bonds.Count; j++)
            {
                var positionA = atomPositions[bonds[j].First];
                var positionB = atomPositions[bonds[j].Second];
                _bondInstance.SetModelMatrix(j, GenerateBondMatrix(positionA, positionB));
            }
        }
        public static Matrix AlignBetween(Vector3 a, Vector3 b)
        {
            Vector3 z = new Vector3(0, 0, 1);
            Vector3 p = b - a;

            float radians = (float)Math.Acos(GetDotProduct(z, p) / GetMagnitude(p));
            float angle = 180 / 3.1415926f * radians;

            var translated = Matrix.Translation(a);

            var mat = Matrix.RotationAxis(Vector3.Cross(z, p), (float)angle);
            return mat * translated;
        }
        public static float GetDotProduct(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z; ;
        }
        public static float GetMagnitude(Vector3 vector)
        {
            return (float)Math.Sqrt(GetDotProduct(vector, vector));
        }


        private void AddAllAtoms()
        {
            var ATOMS = _trajectory.GetTopology().GetAtoms();

            Console.WriteLine("Trajectory consists of {0} atoms.", ATOMS.Count);
            Console.WriteLine("Adding Atoms to Scene...");

            var snapshotZero = _trajectory.GetSnapshot(0);
            Dictionary<char, Pair<int, InstancedModel>> elementMap = new Dictionary<char, Pair<int, InstancedModel>>();
            //elementMap.Reserve(8);

            for (int j = 0; j < ATOMS.Count; j++)
            {
                var atom = ATOMS[j];
                var matrix = GenerateAtomMatrix(snapshotZero.GetPosition(j), atom);
                var element = atom.Element;

                List<Pair<int, InstancedModel>> vals = new List<Pair<int, InstancedModel>>(elementMap.Values);
                if (elementMap.ContainsKey(element)) //already in cache
                {
                    elementMap[element].Second.AddInstance(matrix);
                    _elementIndexes.Add(new ElementIndex(elementMap[element].First,
                        elementMap[element].Second.GetInstanceCount()));
                }
                else //not in cache
                {
                    Console.WriteLine("Generating model type {0}...", element);
                    var model = GenerateAtomModel(atom, matrix);
                    _scene.AddModel(model);

                    elementMap.Add(element, new Pair<int, InstancedModel>(_atomInstances.Count, model));
                    _elementIndexes.Add(new ElementIndex(_atomInstances.Count, 0));
                    _atomInstances.Add(model);
                    Console.WriteLine("... done generating data for {0}", element);
                }
            }

            Console.WriteLine("... done adding atoms for that trajectory.");
        }
        private void AddAllBonds()
        {
            var BONDS = _trajectory.GetTopology().GetBonds();

            Console.WriteLine("Trajectory consists of {0} bonds.", BONDS.Count);
            Console.WriteLine("Adding Bonds to Scene...");

            List<OptionalDataBuffer> list = new List<OptionalDataBuffer>(){new ColorBuffer(BOND_COLOR, 6)};
            _bondInstance = new InstancedModel(GetBondMesh(), list);

            var snapshotZero = _trajectory.GetSnapshot(0);
            foreach (var bond in BONDS)
            {
                var positionA = snapshotZero.GetPosition(bond.First);
                var positionB = snapshotZero.GetPosition(bond.Second);
                _bondInstance.AddInstance(GenerateBondMatrix(positionA, positionB));
            }

            _scene.AddModel(_bondInstance);
            Console.WriteLine("... done adding bonds for that trajectory.");
        }
        private Mesh GetAtomMesh()
        {
            if (_atom != null)
                return _atom;

            Console.Write("Generating atom mesh... ");

            //adapted from sandy_bence's 2005 post over at:
            //http://www.gamedev.net/topic/350823-rendering-a-sphere-using-triangle-strips/

            List<Vector3> vertices = new List<Vector3>();
            for (uint stack = 0; stack <= ATOM_STACKS; stack++)
            {
                for (uint slice = 0; slice < ATOM_SLICES; slice++)
                {
                    float theta = stack * PI / ATOM_STACKS;
                    float phi   = slice * 2 * PI / ATOM_SLICES;

                    float sinTheta = (float)Math.Sin(theta);
                    float cosTheta = (float)Math.Cos(theta);

                    float sinPhi = (float)Math.Sin(phi);
                    float cosPhi = (float)Math.Cos(phi);

                    vertices.Add(new Vector3(
                        cosPhi * sinTheta,
                        sinPhi * sinTheta,
                        cosTheta
                    ));
                }
            }

            List<int> indices = new List<int>();
            for (uint stack = 0; stack < ATOM_STACKS; stack++)
            {
                for (uint slice = 0; slice <= ATOM_SLICES; slice++)
                {
                    var sliceMod = slice % ATOM_SLICES;
                    indices.Add((int)((stack       * ATOM_SLICES) + sliceMod));
                    indices.Add((int)(((stack + 1) * ATOM_SLICES) + sliceMod));
                }
            }

            var vBuffer = new VertexBuffer(vertices);
            var iBuffer = new IndexBuffer(indices, BeginMode.TriangleStrip);
            _atom = new Mesh(vBuffer, iBuffer, BeginMode.TriangleStrip);

            Console.WriteLine("done. Cached the result.");
            return _atom;
        }
        private Mesh GetBondMesh()
        {
            if (_bond != null)
                return _bond;

            Console.Write("Generating bond mesh... ");

            // http://in.answers.yahoo.com/question/index?qid=20060907224537AA8MBBH
            const float sqrt3over2 = 0.86602540378f;
            const float sqrt3over4 = sqrt3over2 / 2;
            List<Vector3> vertices = new List<Vector3>(){
                new Vector3(0,            0, 0),
                new Vector3(1,            0, 0),
                new Vector3(0.5F, sqrt3over2, 0),
                new Vector3(0,            0, 1),
                new Vector3(1,            0, 1),
                new Vector3(0.5F, sqrt3over2, 1)
            };

            List<int> indices = new List<int>(){
                2, 5, 4, 1,
                3, 5, 2, 0,
                1, 4, 3, 0,
                0, 2, 1, 0/*, //this and next line are the end caps
                3, 4, 5, 3*/
            };

            Vector3 OFFSET = new Vector3(0.25F, sqrt3over4, 0);
            //Vector3 vertex;
            //std.Transformation(vertices[0], vertices[vertices.Count-1], vertices[0], out vertex);
            //vertex -= OFFSET;
            /*
               std::transform(vertices.begin(), vertices.end(), vertices.begin(),
                    [&](const glm::vec3& vertex)
                    {
                        return vertex - OFFSET;
                    }
                );
            */

            var vBuffer = new VertexBuffer(vertices);
            var iBuffer = new IndexBuffer(indices, BeginMode.TriangleStrip); // quads
            _bond = new Mesh(vBuffer, iBuffer, BeginMode.TriangleStrip); // quads

            Console.WriteLine("done. Cached the result.");
            return _bond;
        }
        private ColorBuffer GenerateColorBuffer(Atom atom)
        {
            var N_VERTICES = (ATOM_STACKS + 1) * ATOM_SLICES;
            List<Vector3> colors = new List<Vector3>();
            for (int i = 0; i < N_VERTICES; i++) colors.Add(atom.Color);
            
            colors.Add(atom.Color);

            var vertices = GetAtomMesh().GetVertices();
            
            for (int j = 0; j < N_VERTICES; j++)
            {
                float distance = GetMagnitude(vertices[j] - ATOM_LIGHT_POSITION);
                float scaledDistance = distance / ATOM_LIGHT_POWER;
                Vector3 luminosity = ATOM_LIGHT_COLOR * (1 - scaledDistance);

                if (luminosity.X > 0 && luminosity.Y > 0 && luminosity.Z > 0)
                    colors[j] += luminosity;
            }

            return new ColorBuffer(colors);
        }
        private InstancedModel GenerateAtomModel(Atom atom, Matrix matrix)
        {
            List<OptionalDataBuffer> list = new List<OptionalDataBuffer>(){GenerateColorBuffer(atom)};
            return new InstancedModel(GetAtomMesh(), matrix, list);
        }
        private Matrix GenerateAtomMatrix(Vector3 position, Atom atom)
        {
            var matrix = Matrix.Translation(position);
            var shellCount = new Vector3(atom.ElectronShellCount);
            return Matrix.Scaling(new Vector3(ATOM_SCALE) * shellCount) * matrix;
        }
        private Matrix GenerateBondMatrix(Vector3 start, Vector3 end)
        {
            float FLT_EPSILON = 1.19209290E-07F;
            float distance = GetMagnitude(start - end);
            if (distance <= FLT_EPSILON)
                return Matrix.Scaling(new Vector3(new Vector2(BOND_SCALE), FLT_EPSILON));

            var matrix = AlignBetween(start, end);
            return Matrix.Scaling(new Vector3(new Vector2(BOND_SCALE), distance)) * matrix;
        }

        struct ElementIndex
        {
            public int elementIndex;
            public int instanceIndex;

            public ElementIndex(int elIndex, int instIndex)
            {
                elementIndex = elIndex;
                instanceIndex = instIndex;
            }
        }

        Mesh _atom = null, _bond = null;
        Scene _scene;
        Trajectory _trajectory;
        List<ElementIndex> _elementIndexes = new List<ElementIndex>(); //references specific instances
        List<InstancedModel> _atomInstances = new List<InstancedModel>();
        InstancedModel _bondInstance;
        int _transitionTime; //how much elapsed time between each snapshot
        int _snapshotIndexA, _snapshotIndexB; //interpolate between these
    }
}
