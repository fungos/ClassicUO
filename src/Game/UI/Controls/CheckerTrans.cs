﻿#region license
//  Copyright (C) 2018 ClassicUO Development Community on Github
//
//	This project is an alternative client for the game Ultima Online.
//	The goal of this is to develop a lightweight client considering 
//	new technologies.  
//      
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
#endregion

using System;

using ClassicUO.Renderer;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClassicUO.Game.UI.Controls
{
    internal class CheckerTrans : Control
    {
        private static SpriteTexture _transparentTexture/*, _transparentTexture2*/;

        //public CheckerTrans(float alpha = 0.5f)
        //{
        //    _alpha = alpha;
        //    AcceptMouseInput = false;
        //}

        public CheckerTrans(string[] parts)
        {
            X = int.Parse(parts[1]);
            Y = int.Parse(parts[2]);
            Width = int.Parse(parts[3]);
            Height = int.Parse(parts[4]);
            AcceptMouseInput = false;

        }

        public static SpriteTexture TransparentTexture
        {
            get
            {
                if (_transparentTexture == null || _transparentTexture.IsDisposed)
                {
                    _transparentTexture = new SpriteTexture(1, 1);

                    _transparentTexture.SetData(new Color[1]
                    {
                        Color.Black
                    });
                }

                _transparentTexture.Ticks = Engine.Ticks;

                return _transparentTexture;
            }
        }

        //public static SpriteTexture TransparentTexture2
        //{
        //    get
        //    {
        //        if (_transparentTexture2 == null || _transparentTexture2.IsDisposed)
        //        {
        //            _transparentTexture2 = new SpriteTexture(1, 1);

        //            _transparentTexture2.SetData(new Color[1]
        //            {
        //                Color.Transparent
        //            });
        //        }

        //        _transparentTexture2.Ticks = Engine.Ticks;

        //        return _transparentTexture2;
        //    }
        //}

        //private static readonly Lazy<DepthStencilState> _checkerStencil = new Lazy<DepthStencilState>(() =>
        //{
        //    DepthStencilState state = new DepthStencilState();

        //    //state.DepthBufferEnable = true;
        //    //state.StencilEnable = true;

        //    state.StencilFunction = CompareFunction.Always;
        //    state.ReferenceStencil = 1;
        //    state.StencilMask = 1;
        //    state.StencilWriteMask = 1;

        //    state.StencilFail = StencilOperation.Keep;
        //    state.StencilDepthBufferFail = StencilOperation.Keep;
        //    state.StencilPass = StencilOperation.Replace;


        //    state.CounterClockwiseStencilPass = StencilOperation.Replace;


            

        //    return state;
        //});

        //private static readonly Lazy<DepthStencilState> _checkerStencil2 = new Lazy<DepthStencilState>(() =>
        //{
        //    DepthStencilState state = new DepthStencilState
        //    {
        //        StencilFunction = CompareFunction.NotEqual,
        //        StencilMask = 1,
        //        ReferenceStencil = 1,
        //        StencilFail = StencilOperation.Keep,
        //        StencilDepthBufferFail = StencilOperation.Keep,
        //        StencilPass = StencilOperation.Keep
        //    };



        //    return state;
        //});

        //private static readonly Lazy<BlendState> _checkerBlend = new Lazy<BlendState>(() =>
        //{
        //    BlendState blend = new BlendState();
        //    blend.ColorWriteChannels = ColorWriteChannels.None;
        //    blend.ColorWriteChannels1 = ColorWriteChannels.None;
        //    blend.ColorWriteChannels2 = ColorWriteChannels.None;
        //    blend.ColorWriteChannels3 = ColorWriteChannels.None;
        //    return blend;
        //});

        public override bool Draw(Batcher2D batcher, Point position, Vector3? hue = null)
        {
            //batcher.SetBlendState(_checkerBlend.Value);
            //batcher.SetStencil(_checkerStencil.Value);

            ////Batcher2D.glDisable(0x0DE1);
            //batcher.Draw2D(TransparentTexture2, new Rectangle(position.X, position.Y, Width, Height), Vector3.Zero /*ShaderHuesTraslator.GetHueVector(0, false, 0.5f, false)*/);
            ////Batcher2D.glEnable(0x0DE1);

            //batcher.SetBlendState(null);
            //batcher.SetStencil(null);

            //return true;

            return batcher.Draw2D(TransparentTexture, new Rectangle(position.X, position.Y, Width, Height), ShaderHuesTraslator.GetHueVector(0, false, .5f, false));
        }
    }
}