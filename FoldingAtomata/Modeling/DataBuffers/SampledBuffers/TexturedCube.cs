using FoldingAtomata.Modeling.Shading;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using XNA;

namespace FoldingAtomata.Modeling.DataBuffers.SampledBuffers
{
    public class TexturedCube : OptionalDataBuffer
    {
        public TexturedCube(
            Image positiveX, Image negativeX,
            Image positiveY, Image negativeY,
            Image positiveZ, Image negativeZ)
        {
            _pX = positiveX;
            _pY = positiveY;
            _pZ = positiveZ;
            _nX = negativeX;
            _nY = negativeY;
            _nZ = negativeZ;
        }
        public void MapTo(TextureTarget target, Image img)
        {
            Bitmap bitmap = new Bitmap(img);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, 
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            GL.TexImage2D(target, 0, PixelInternalFormat.Rgba, img.Width, img.Width, 0
                , OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0); //image must be square

            bitmap.UnlockBits(data);
        }

        public void Store(int programHandle)
        {
            GL.GenTextures(1, out _cubeTexture);
            GL.BindTexture(TextureTarget.TextureCubeMap, _cubeTexture);
            GL.GenerateMipmap(GenerateMipmapTarget.TextureCubeMap);
            GL.TexParameter(TextureTarget.TextureCubeMap, 
                TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, 
                TextureParameterName.TextureMinFilter, (int)All.Linear);
            //glActiveTexture(GL_TEXTURE0);

            MapTo(TextureTarget.TextureCubeMapPositiveX, _pX);
            MapTo(TextureTarget.TextureCubeMapNegativeX, _nX);
            MapTo(TextureTarget.TextureCubeMapPositiveY, _pY);
            MapTo(TextureTarget.TextureCubeMapNegativeY, _nY);
            MapTo(TextureTarget.TextureCubeMapPositiveZ, _pZ);
            MapTo(TextureTarget.TextureCubeMapNegativeZ, _nZ);

            _textureCoordinates = GL.GetAttribLocation(programHandle, "UV");
            if (_textureCoordinates == -1)
                throw new Exception("Could not bind UV attribute");
        }
        public void Enable()
        {
            GL.VertexAttribPointer(_textureCoordinates, 3, VertexAttribPointerType.Float, false, 0, 0);
        }
        public void Disable()
        {
            
        }
        public ShaderSnippet GetVertexShaderGLSL()
        {
            return new ShaderSnippet(
                @"
                    //TexturedCube fields
                    attribute vec3 UV;
                    varying vec3 UVf;
                ",
                @"
                    //TexturedCube methods
                ",
                @"
                    //TexturedCube main method code
                    UVf = UV;
                "
            );
        }
        public ShaderSnippet GetFragmentShaderGLSL()
        {
            return new ShaderSnippet(
                @"
                    //TexturedCube fields
                    uniform samplerCube cubeSampler;
                    varying vec3 UVf;
                ",
                @"
                    //TexturedCube methods
                ",
                @"
                    //TexturedCube main method code
                    colors.material = textureCube(cubeSampler, UVf).rgb;
                "
            );
        }

        Image _pX, _nX, _pY, _nY, _pZ, _nZ;
        uint _cubeTexture;
        int _textureCoordinates;
    }
}
