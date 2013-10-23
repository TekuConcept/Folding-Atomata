using FoldingAtomata.Modeling.DataBuffers;
using FoldingAtomata.Modeling.NMesh;
using FoldingAtomata.Modeling.Shading;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.Modeling
{
    public class InstancedModel
    {
        public InstancedModel(Mesh mesh)
        {
            _mesh = mesh;
            _cachedHandle = 0;
            _isVisible = true;
        }
        public InstancedModel(Mesh mesh, Matrix modelMatrix) : this(mesh)
        {
            _modelMatricies.Add(modelMatrix);
        }
        public InstancedModel(Mesh mesh, List<OptionalDataBuffer> optionalDBs) : this(mesh)
        {
            _optionalDBs = optionalDBs;
        }
        public InstancedModel(Mesh mesh, Matrix modelMatrix, List<OptionalDataBuffer> optionalDBs) : this(mesh, modelMatrix)
        {
            _optionalDBs = optionalDBs;
        }
        public InstancedModel(Mesh mesh, List<Matrix> modelMatricies, List<OptionalDataBuffer> optionalDBs) : this(mesh)
        {
            _modelMatricies = modelMatricies;
            _optionalDBs = optionalDBs;
        }
        public virtual void SaveAs(int programHandle)
        {
            Console.Write("Storing Model under Program {0} [", programHandle);
            _mesh.Store(programHandle);

            Console.Write(_mesh.GetType().Name + " ");

            foreach (var buffer in _optionalDBs)
            {
                buffer.Store(programHandle);
                Console.Write(buffer.GetType().Name + " ");
            }

            Console.WriteLine("]");
            ShaderProgram.CheckGLError();
        }
        public void AddInstance(Matrix instanceModelMatrix)
        {
            _modelMatricies.Add(instanceModelMatrix);
        }
        public void Render(int programHandle)
        {
            if (_cachedHandle != programHandle) //bit of adaptable caching
            {
                _cachedHandle = programHandle;
                _matrixModelLocation = GL.GetUniformLocation(programHandle, "modelMatrix");
            }

            if (_isVisible)
            {
                EnableDataBuffers();

                foreach (Matrix modelMatrix in _modelMatricies)
                {
                    GL.UniformMatrix4(_matrixModelLocation, 1, false, Utils.XNA_Float_Matrix(modelMatrix));
                    _mesh.Draw();
                }
            }
        }
        public void SetModelMatrix(int index, Matrix matrix)
        {
            _modelMatricies[index] = matrix;
        }
        public void SetVisible(bool visible)
        {
            _isVisible = visible;
        }
        public List<OptionalDataBuffer> GetOptionalDataBuffers()
        {
            return _optionalDBs;
        }
        public int GetInstanceCount()
        {
            return _modelMatricies.Count;
        }


        private void EnableDataBuffers()
        {
            _mesh.Enable();

            foreach (var buffer in _optionalDBs)
                buffer.Enable();
        }
        private void DisableDataBuffers()
        {
            _mesh.Disable();

            foreach (var buffer in _optionalDBs)
                buffer.Disable();
        }


        Mesh _mesh;
        List<Matrix> _modelMatricies = new List<Matrix>();
        List<OptionalDataBuffer> _optionalDBs = new List<OptionalDataBuffer>();
        int _cachedHandle;
        int _matrixModelLocation;
        bool _isVisible;
    }
}
