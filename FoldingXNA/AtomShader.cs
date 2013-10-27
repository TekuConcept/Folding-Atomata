using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace FoldingXNA
{
    public class AtomShader : BasicEffect
    {
        public AtomShader()
        {
            base.AddMeshToScene(Mesh.SphereVerticies, Mesh.SphereIndices, BeginMode.TriangleStrip, "atom");
            base.AddMeshToScene(Mesh.PrismVerticies, Mesh.PrismIndices, BeginMode.TriangleStrip, "bond");

            var p = base._proj;
            var m = new XNA.Matrix(
                p.M11, p.M12, p.M13, p.M14,
                p.M21, p.M22, p.M23, p.M24,
                p.M31, p.M32, p.M33, p.M34,
                p.M41, p.M42, p.M43, p.M44);
            frustum = new XNA.BoundingFrustum(m);
        }

        public List<Atom> Atoms { get; set; }
        public List<BondID> Bonds { get; set; }
        XNA.BoundingFrustum frustum;

        public new Matrix4 View
        {
            get
            {
                return base.View;
            }
            set
            {
                base.View = value;

                var p = value;
                var q = base.Projection;
                var m1 = new XNA.Matrix(
                    p.M11, p.M12, p.M13, p.M14, 
                    p.M21, p.M22, p.M23, p.M24, 
                    p.M31, p.M32, p.M33, p.M34, 
                    p.M41, p.M42, p.M43, p.M44);
                var m2 = new XNA.Matrix(
                    q.M11, q.M12, q.M13, q.M14,
                    q.M21, q.M22, q.M23, q.M24,
                    q.M31, q.M32, q.M33, q.M34,
                    q.M41, q.M42, q.M43, q.M44);
                frustum = new XNA.BoundingFrustum(m1*m2);

            }
        }
        
        public new void Draw()
        {
            SetScene();

            for (int x = 0; x < Atoms.Count; x++ )
            {
                Vector3 v = Atoms[x].KeyFrames[0];
                if (frustum.Contains(new XNA.Vector3(v.X, v.Y, v.Z)) == XNA.ContainmentType.Contains)
                {
                    var m = Atom.GetScale(Atom.GetScaleValue(Atoms[x].Number)) * Matrix4.CreateTranslation(v) * _view;
                    Renderer.ApplyMatrix(m, viewID);
                    Renderer.ApplyColor(Atoms[x].Color, colorID);
                    Renderer.Draw(_info["atom"].Mode, DrawElementsType.UnsignedInt, _info["atom"].Count, _info["atom"].VertID, _info["atom"].IndcID);
                }
            }

            for (int y = 0; y < Bonds.Count; y++)
            {
                var v1 = Atoms[Bonds[y].A].KeyFrames[0];
                var v2 = Atoms[Bonds[y].B].KeyFrames[0];
                if (frustum.Contains(new XNA.Vector3(v1.X, v1.Y, v1.Z)) == XNA.ContainmentType.Contains)
                {
                    //var m = Matrix4.CreateTranslation(v1) * Matrix4.Scale(0.06F, 0.06F, Utils.GetMagnitude(v2-v1));
                    var m = Utils.GenerateBondMatrix(v1, v2) * _view;
                    Renderer.ApplyMatrix(m, viewID);
                    Renderer.ApplyColor(OpenTK.Graphics.Color4.MintCream, colorID);
                    Renderer.Draw(_info["bond"].Mode, DrawElementsType.UnsignedInt, _info["bond"].Count, _info["bond"].VertID, _info["bond"].IndcID);
                }
            }
        }
    }
}
