using System;
using System.Collections.Generic;
using System.Text;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace HotkeyExperiment
{
    /// <summary>
    /// Minimal functionality to access DirectX to determine the current
    /// HDR/advanced color state of the display.
    /// </summary>
    class DXManager
    {
        SharpDX.Direct3D11.Device D3DDevice;
        SwapChain1 DxgiSwapChain;
        Output6 DxgiOutput6;

        public DXManager(IntPtr windowHandle)
        {
            SharpDX.Direct3D.FeatureLevel[] featureLevels =
            {
                SharpDX.Direct3D.FeatureLevel.Level_11_0 // Minimum requirement for HDR support.
            };

            D3DDevice = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, featureLevels);

            SwapChainDescription1 desc = new SwapChainDescription1
            {
                AlphaMode = AlphaMode.Ignore,
                BufferCount = 2,
                Flags = SwapChainFlags.None,
                Format = Format.R10G10B10A2_UNorm, // HDR10
                Height = 16, // Placeholder size
                Width = 16,
                SampleDescription = new SampleDescription { Count = 1, Quality = 0 },
                Scaling = Scaling.None,
                Stereo = false,
                SwapEffect = SwapEffect.FlipDiscard,
                Usage = Usage.RenderTargetOutput
            };

            Factory2 factory = new Factory2();

            SwapChain1 sc1 = new SwapChain1(factory, D3DDevice, ref desc);
            
        }
    }
}
