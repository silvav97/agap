using System.Runtime.InteropServices;

namespace Agap.Backemd.Data
{
    public interface IRuntimeInformationWrapper
    {
        bool IsOSPlatform(OSPlatform osPlatform);
    }
}
