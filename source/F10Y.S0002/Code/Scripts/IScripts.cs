using System;
using System.Linq;
using System.Threading.Tasks;

using F10Y.T0005;


namespace F10Y.S0002
{
    [ScriptsMarker]
    public partial interface IScripts :
        T0014.T004.IScriptTextOutputInfrastructure_Implementation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Hard to get cross platform information, especially for installed RAM! (See <see href="https://github.com/dotnet/runtime/issues/22948"/>.)
        /// My attempts:
        /// <list type="bullet">
        /// <item>D8S.E0020.Private - An experiment in using the Microsoft.VisualBasic assembly</item>
        /// <item>D8S.E0021.Private - An experiment in using the System.Management assembly</item>
        /// <item>D8S.E0022.Private - An experiment in using PInvoke on Windows</item>
        /// </list>
        /// </remarks>
        async Task Display_MachineInformation()
        {
            /// Run.
            var machineName = Instances.MachineNameOperator.Get_MachineName();

            var processorCount = Instances.EnvironmentOperator.Get_ProcessorCount_Logical();

            // 4096, this is useless.
            var systemPageSize = Instances.EnvironmentOperator.Get_SystemPageSize_InBytes();

            // True
            var is64BitOS = Instances.EnvironmentOperator.Is_64BitOperatingSystem();

            // Microsoft Windows NT 10.0.19045.0
            var OSVersion = Instances.EnvironmentOperator.Get_OperatingSystem();

            // C:\WINDOWS\system32
            var systemDirectoryPath = Instances.EnvironmentOperator.Get_SystemDirectoryPath();

            // David
            var userName = Instances.EnvironmentOperator.Get_UserName();

            // VANESSA
            var userDomainName = Instances.EnvironmentOperator.Get_UserDomainName();

            var appData_DirectoryPath = Instances.EnvironmentOperator.Get_SpecialDirectoryPath(Environment.SpecialFolder.ApplicationData);
            // Useless:
            //var appData_Common_DirectoryPath = Instances.EnvironmentOperator.Get_SpecialDirectoryPath(Environment.SpecialFolder.CommonApplicationData); // C:\ProgramData
            var appData_Local_DirectoryPath = Instances.EnvironmentOperator.Get_SpecialDirectoryPath(Environment.SpecialFolder.LocalApplicationData);
            var user_DirectoryPath = Instances.EnvironmentOperator.Get_SpecialDirectoryPath(Environment.SpecialFolder.UserProfile);

            // 8.0.1
            var clrVersion = Instances.EnvironmentOperator.Get_CLR_Version();

            // True
            var is64BitProcess = Instances.EnvironmentOperator.Is_64BitProcess();

            // 28631040, 28 MB
            var workingSet = Instances.EnvironmentOperator.Get_WorkingSet();

            // C:\Code\DEV\Git\GitHub\davidcoats\D8S.S0000.Private\source\D8S.S0000\bin\Debug\net8.0
            var currentDirectoryPath = Instances.EnvironmentOperator.Get_CurrentDirectoryPath();

            var totalAvailableMemoryBytes = Instances.EnvironmentOperator.Get_TotalAvailableMemory_InBytes();

            var totalAvailableMemoryBytes_GiB = Instances.BytesOperator.Get_Gibibytes_AsDouble(totalAvailableMemoryBytes);

            var drives = Instances.EnvironmentOperator.Get_Drives();

            var lines_ForDrives = Instances.EnumerableOperator.From("Drives:")
                .Append(drives
                    .SeparateMany_Lines(drive =>
                    {
                        var drive_Name = Instances.DriveInfoOperator.Get_Name(drive);

                        var size_GiB = Instances.DriveInfoOperator.Get_Size_InGibibytes_AsDouble(drive);
                        var freeSpace_GiB = Instances.DriveInfoOperator.Get_FreeSpace_InGibibytes_AsDouble(drive);

                        var output = Instances.EnumerableOperator.From($"{drive_Name}:")
                            .Append(Instances.EnumerableOperator.Empty<string>()
                                .Append($"\t{size_GiB:0.0} {Instances.UnitSymbols.Gibibyte}: total size")
                                .Append($"\t{freeSpace_GiB:0.0} {Instances.UnitSymbols.Gibibyte}: total free space")
                            )
                            .Entab()
                            ;

                        return output;
                    })
                )
                ;

            var specialDirectoryPaths_ByStringRepresentation = Instances.EnvironmentOperator.Get_SpecialDirectoryPaths_BySpecialDirectory();

            var lines_ForSpecialDirectories = Instances.EnumerableOperator.From("Special Directory Paths:")
                .Append_BlankLine()
                .Append(specialDirectoryPaths_ByStringRepresentation
                    .Order_AlphabeticallyBy(pair => pair.Value)
                    .Select(pair => $"{pair.Value}: {pair.Key}")
                    .Entab()
                )
                ;

            var lines_ForOutput = Instances.EnumerableOperator.From("System information")
                .Append_BlankLine()
                .Append("Machine:")
                .Append(Instances.EnumerableOperator.Empty<string>()
                    .Append($"{machineName}: machine name")
                    .Append($"{processorCount}: processor count")
                    .Append($"{totalAvailableMemoryBytes_GiB:0.0} {Instances.UnitSymbols.Gibibyte}: total available memory")
                    .Append(lines_ForDrives)
                    .Append($"{systemPageSize}: system page size")
                    .Entab()
                )
                .Append_BlankLine()
                .Append("Operating System:")
                .Append(Instances.EnumerableOperator.Empty<string>()
                    .Append($"{is64BitOS}: is 64-bit OS")
                    .Append($"{OSVersion}: OS version")
                    .Append($"{systemDirectoryPath}: system directory")
                    .Entab()
                )
                .Append_BlankLine()
                .Append("User:")
                .Append(Instances.EnumerableOperator.Empty<string>()
                    .Append($"{userName}: user name")
                    .Append($"{userDomainName}: user domain name")
                    .Append($"{user_DirectoryPath}: user directory path")
                    .Append($"{appData_DirectoryPath}: application data directory path")
                    //.Append($"{appData_Common_DirectoryPath}: application data (common) directory path")
                    .Append($"{appData_Local_DirectoryPath}: application data (local) directory path")
                    .Entab()
                )
                .Append_BlankLine()
                .Append(".NET Runtime:")
                .Append($"\t{clrVersion}: Common Language Runtime (CLR) version")
                .Append_BlankLine()
                .Append("Process:")
                .Append($"\t{is64BitProcess}: is 64-bit process")
                .Append($"\t{workingSet}: working set")
                .Append($"\t{currentDirectoryPath}: current directory")
                .Append_BlankLine()
                .Append(lines_ForSpecialDirectories)
                ;

            Instances.ConsoleOperator.Write_Lines(lines_ForOutput);

            await this.Write_Lines_AndOpen(lines_ForOutput);
        }
    }
}
