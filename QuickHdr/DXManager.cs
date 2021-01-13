// #define USE_DX_DEBUG

using System;
using System.Collections.Generic;
using System.Text;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace QuickHdr
{
    /// <summary>
    /// Minimal functionality to access DirectX to determine the current
    /// HDR/advanced color state of the display.
    /// </summary>
    /// <remarks>
    /// TODO: Not multithreading safe.
    /// </remarks>
    class DXManager
    {
        SharpDX.Direct3D11.Device D3DDevice;
        Factory2 DxgiFactory;
        IntPtr Hwnd;

        public DXManager(IntPtr windowHandle)
        {
            Hwnd = windowHandle;

            CreateDeviceDependentResources();
        }

        private void CreateDeviceDependentResources()
        {
            SharpDX.Direct3D.FeatureLevel[] featureLevels =
            {
                SharpDX.Direct3D.FeatureLevel.Level_11_0 // This is practically the min requirement for HDR support.
            };

#if DEBUG && USE_DX_DEBUG
            DeviceCreationFlags flags = DeviceCreationFlags.Debug;
            // This requires Native Debugging to be enabled to have any use.
#else
            DeviceCreationFlags flags = DeviceCreationFlags.None;
#endif
            {

            }

            D3DDevice = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Hardware, flags, featureLevels);

            DxgiFactory = new Factory2();
        }

        public bool IsHdrActive()
        {
            if (DxgiFactory.IsCurrent == false)
            {
                CreateDeviceDependentResources();
            }

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

            // TODO: Swapchain creation is fairly expensive, optimize this with the D3D12HDR method.
            using (SwapChain1 sc1 = new SwapChain1(DxgiFactory, D3DDevice, Hwnd, ref desc))
            {
                Output6 output6 = sc1.ContainingOutput.QueryInterface<Output6>();

                switch (output6.Description1.ColorSpace)
                {
                    case ColorSpaceType.RgbFullG2084NoneP2020:
                        return true;

                    case ColorSpaceType.RgbFullG22NoneP709:
                        return false;

                    default:
                        // Unexpected!
                        return false;
                }
            }
        }
    }
}
