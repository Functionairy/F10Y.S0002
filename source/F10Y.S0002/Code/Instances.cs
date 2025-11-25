using System;


namespace F10Y.S0002
{
    public static class Instances
    {
        public static L0001.L000.IBytesOperator BytesOperator => L0001.L000.BytesOperator.Instance;
        public static L0000.IConsoleOperator ConsoleOperator => L0000.ConsoleOperator.Instance;
        public static L0001.L000.IDriveInfoOperator DriveInfoOperator => L0001.L000.DriveInfoOperator.Instance;
        public static L0000.IEnumerableOperator EnumerableOperator => L0000.EnumerableOperator.Instance;
        public static L0061.IEnvironmentOperator EnvironmentOperator => L0061.EnvironmentOperator.Instance;
        public static L0000.IMachineNameOperator MachineNameOperator => L0000.MachineNameOperator.Instance;
        public static L0001.L000.IUnitSymbols UnitSymbols => L0001.L000.UnitSymbols.Instance;
    }
}