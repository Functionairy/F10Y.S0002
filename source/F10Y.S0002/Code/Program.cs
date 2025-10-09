using System;
using System.Threading.Tasks;


namespace F10Y.S0002
{
    class Program
    {
        static async Task Main()
        {
            await Scripts.Instance
                .Display_MachineInformation()
                ;
        }
    }
}