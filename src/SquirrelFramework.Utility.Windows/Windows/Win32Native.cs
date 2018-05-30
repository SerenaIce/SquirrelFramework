namespace SquirrelFramework.Utility.Windows
{
    #region using directives

    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    #endregion

    /// <summary>
    /// This class is a win32 API wrapper class for P/I
    /// </summary>
    internal static class Win32Native
    {
        #region -- Constants --

        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_LOGON_NETWORK = 3;
        public const int LOGON32_LOGON_BATCH = 4;
        public const int LOGON32_LOGON_SERVICE = 5;
        public const int LOGON32_LOGON_UNLOCK = 7;
        public const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
        public const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

        public const int LOGON32_PROVIDER_DEFAULT = 0;
        public const int LOGON32_PROVIDER_WINNT35 = 1;
        public const int LOGON32_PROVIDER_WINNT40 = 2;
        public const int LOGON32_PROVIDER_WINNT50 = 3;

        public const int SecurityImpersonationLevelAnonymous = 0;
        public const int SecurityImpersonationLevelIdentification = 1;
        public const int SecurityImpersonationLevelImpersonation = 2;
        public const int SecurityImpersonationLevelDelegation = 3;

        #endregion

        #region -- ADVAPI32.DLL --

        public enum SidNameUse
        {
            SidTypeAlias = 4,
            SidTypeComputer = 9,
            SidTypeDeletedAccount = 6,
            SidTypeDomain = 3,
            SidTypeGroup = 2,
            SidTypeInvalid = 7,
            SidTypeUnknown = 8,
            SidTypeUser = 1,
            SidTypeWellKnownGroup = 5
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SecurityAttributes
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StartupInfo
        {
            public int cb;
            public String lpReserved;
            public String lpDesktop;
            public String lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ProcessInformation
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LsaUnicodeString
        {
            public UInt16 Length;
            public UInt16 MaximumLength;
            public IntPtr Buffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LsaObjectAttributes
        {
            public int Length;
            public IntPtr RootDirectory;
            public LsaUnicodeString ObjectName;
            public UInt32 Attributes;
            public IntPtr SecurityDescriptor;
            public IntPtr SecurityQualityOfService;
        }

        public enum LSA_AccessPolicy : long
        {
            POLICY_VIEW_LOCAL_INFORMATION = 0x00000001L,
            POLICY_VIEW_AUDIT_INFORMATION = 0x00000002L,
            POLICY_GET_PRIVATE_INFORMATION = 0x00000004L,
            POLICY_TRUST_ADMIN = 0x00000008L,
            POLICY_CREATE_ACCOUNT = 0x00000010L,
            POLICY_CREATE_SECRET = 0x00000020L,
            POLICY_CREATE_PRIVILEGE = 0x00000040L,
            POLICY_SET_DEFAULT_QUOTA_LIMITS = 0x00000080L,
            POLICY_SET_AUDIT_REQUIREMENTS = 0x00000100L,
            POLICY_AUDIT_LOG_ADMIN = 0x00000200L,
            POLICY_SERVER_ADMIN = 0x00000400L,
            POLICY_LOOKUP_NAMES = 0x00000800L,
            POLICY_NOTIFICATION = 0x00001000L
        }

        [DllImport("advapi32.dll")]
        public static extern uint LsaNtStatusToWinError(uint status);

        [DllImport("advapi32.dll")]
        public static extern IntPtr FreeSid(IntPtr pSid);

        [DllImport("advapi32.dll")]
        public static extern uint LsaClose(IntPtr objectHandle);

        [DllImport("advapi32.dll", PreserveSig = true)]
        public static extern UInt32 LsaOpenPolicy(ref LsaUnicodeString systemName, ref LsaObjectAttributes objectAttributes, Int32 desiredAccess, out IntPtr policyHandle);

        [DllImport("advapi32.dll", SetLastError = true, PreserveSig = true)]
        public static extern uint LsaAddAccountRights(IntPtr policyHandle, IntPtr accountSid, LsaUnicodeString[] userRights, uint countOfRights);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool LookupAccountSid(string systemName, byte[] bSid, StringBuilder name, ref int cbName, StringBuilder domainName, ref int cbDomainName, ref SidNameUse peUse);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool LookupAccountName(string systemName, StringBuilder name, byte[] bSid, ref int cbName, StringBuilder domainName, ref int cbDomainName, ref SidNameUse peUse);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool LookupAccountName(string systemName, string name, IntPtr psid, ref int cbName, StringBuilder domainName, ref int cbDomainName, ref  int peUse);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool ConvertSidToStringSid(byte[] sid, out StringBuilder stringSid);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool LogonUserW(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DuplicateTokenEx(IntPtr tokenHandle, int dwDesiredAccess, ref SecurityAttributes lpTokenAttributes, int securityImpersonationLevel, int TOKEN_TYPE, ref IntPtr dupeTokenHandle);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CreateProcessAsUserW(IntPtr hToken, String lpApplicationName, String lpCommandLine, ref SecurityAttributes lpProcessAttributes, ref SecurityAttributes lpThreadAttributes, bool bInheritHandles, int dwCreationFlags, IntPtr lpEnvironment, String lpCurrentDirectory, ref StartupInfo lpStartupInfo, ref ProcessInformation lpProcessInformation);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern Int32 RegCloseKey(IntPtr hKey);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyEx")]
        public static extern Int32 RegOpenKeyEx(IntPtr hKey, String subKey, UInt32 options, Int32 sam, out IntPtr phkResult);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW", SetLastError = true)]
        public static extern Int32 RegQueryValueEx(IntPtr hKey, String lpValueName, IntPtr lpReserved, out UInt32 lpType, StringBuilder lpData, ref UInt32 lpcbData);

        [DllImport("advapi32.dll", EntryPoint = "OpenEventLog")]
        public static extern IntPtr OpenEventLog(string lpUNCServerName, String lpSourceName);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool BackupEventLog(IntPtr hEventLog, string backupFile);

        [DllImport("advapi32.dll", EntryPoint = "CloseEventLog")]
        public static extern bool CloseEventLog(IntPtr hEventLog);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int ImpersonateLoggedOnUser(IntPtr hToken);

        #endregion

        #region -- KERNEL32.DLL --

        [StructLayout(LayoutKind.Sequential)]
        public class OsVersionInfo
        {
            private Int32 _dwOSVersionInfoSize;

            public Int32 dwOSVersionInfoSize
            {
                get { return this._dwOSVersionInfoSize; }
                set { this._dwOSVersionInfoSize = value; }
            }
            private Int32 _dwMajorVersion;

            public Int32 dwMajorVersion
            {
                get { return this._dwMajorVersion; }
                set { this._dwMajorVersion = value; }
            }
            private Int32 _dwMinorVersion;

            public Int32 dwMinorVersion
            {
                get { return this._dwMinorVersion; }
                set { this._dwMinorVersion = value; }
            }
            private Int32 _dwBuildNumber;

            public Int32 dwBuildNumber
            {
                get { return this._dwBuildNumber; }
                set { this._dwBuildNumber = value; }
            }
            private Int32 _dwPlatformId;

            public Int32 dwPlatformId
            {
                get { return this._dwPlatformId; }
                set { this._dwPlatformId = value; }
            }
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            private String _szCSDVersion;

            public String szCSDVersion
            {
                get { return this._szCSDVersion; }
                set { this._szCSDVersion = value; }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class OsVersionInfoEx : OsVersionInfo
        {
            private Int16 _wServicePackMajor;

            public Int16 wServicePackMajor
            {
                get { return this._wServicePackMajor; }
                set { this._wServicePackMajor = value; }
            }
            private Int16 _wServicePackMinor;

            public Int16 wServicePackMinor
            {
                get { return this._wServicePackMinor; }
                set { this._wServicePackMinor = value; }
            }
            private Int16 _wSuiteMask;

            public Int16 wSuiteMask
            {
                get { return this._wSuiteMask; }
                set { this._wSuiteMask = value; }
            }
            private Byte _wProductType;

            public Byte wProductType
            {
                get { return this._wProductType; }
                set { this._wProductType = value; }
            }
            private Byte _wReserved;

            public Byte wReserved
            {
                get { return this._wReserved; }
                set { this._wReserved = value; }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MemoryStatus
        {
            private UInt32 dwLength;

            public UInt32 DwLength
            {
                get { return this.dwLength; }
                set { this.dwLength = value; }
            }
            private UInt32 dwMemoryLoad;

            public UInt32 DwMemoryLoad
            {
                get { return this.dwMemoryLoad; }
                set { this.dwMemoryLoad = value; }
            }
            private UInt32 dwTotalPhys;

            public UInt32 DwTotalPhys
            {
                get { return this.dwTotalPhys; }
                set { this.dwTotalPhys = value; }
            }
            private UInt32 dwAvailPhys;

            public UInt32 DwAvailPhys
            {
                get { return this.dwAvailPhys; }
                set { this.dwAvailPhys = value; }
            }
            private UInt32 dwTotalPageFile;

            public UInt32 DwTotalPageFile
            {
                get { return this.dwTotalPageFile; }
                set { this.dwTotalPageFile = value; }
            }
            private UInt32 dwAvailPageFile;

            public UInt32 DwAvailPageFile
            {
                get { return this.dwAvailPageFile; }
                set { this.dwAvailPageFile = value; }
            }
            private UInt32 dwTotalVirtual;

            public UInt32 DwTotalVirtual
            {
                get { return this.dwTotalVirtual; }
                set { this.dwTotalVirtual = value; }
            }
            private UInt32 dwAvailVirtual;

            public UInt32 DwAvailVirtual
            {
                get { return this.dwAvailVirtual; }
                set { this.dwAvailVirtual = value; }
            }
        }

        [DllImport("kernel32.dll")]
        public extern static Boolean GetVersionEx([In, Out] OsVersionInfo versionInfo);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)]string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean IsWow64Process(IntPtr hProcess, out Boolean wow64Process);

        [DllImport("Kernel32.dll")]
        public static extern Boolean GetExitCodeProcess(IntPtr hProcess, ref uint lpExitCode);

        [DllImport("Kernel32.dll")]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("Kernel32.dll")]
        public static extern Boolean QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        public static extern Boolean QueryPerformanceFrequency(out long lpFrequency);

        [DllImport("kernel32.dll")]
        public static extern int GetLastError();

        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MemoryStatus buf);

        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        [DllImport("kernel32.dll ", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)]     string path, [MarshalAs(UnmanagedType.LPTStr)]     StringBuilder shortPath, Int32 shortPathLength);

        #endregion

        #region --USER32.DLL

        [DllImport("user32.dll", EntryPoint = "GetGuiResources")]
        public static extern UInt32 GetGuiResources([In] IntPtr hProcess, UInt32 uiFlags);

        #endregion

        #region --NETAPI32.DLL--

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ShareInfo
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            private string _shi2_netname;

            public string shi2_netname
            {
                get { return this._shi2_netname; }
                set { this._shi2_netname = value; }
            }
            private uint _shi2_type;

            public uint shi2_type
            {
                get { return this._shi2_type; }
                set { this._shi2_type = value; }
            }
            [MarshalAs(UnmanagedType.LPWStr)]
            private string _shi2_remark;

            public string shi2_remark
            {
                get { return this._shi2_remark; }
                set { this._shi2_remark = value; }
            }
            private uint _shi2_permissions;

            public uint shi2_permissions
            {
                get { return this._shi2_permissions; }
                set { this._shi2_permissions = value; }
            }
            private uint _shi2_max_uses;

            public uint shi2_max_uses
            {
                get { return this._shi2_max_uses; }
                set { this._shi2_max_uses = value; }
            }
            private uint _shi2_current_uses;

            public uint shi2_current_uses
            {
                get { return this._shi2_current_uses; }
                set { this._shi2_current_uses = value; }
            }
            [MarshalAs(UnmanagedType.LPWStr)]
            private string _shi2_path;

            public string shi2_path
            {
                get { return this._shi2_path; }
                set { this._shi2_path = value; }
            }
            [MarshalAs(UnmanagedType.LPWStr)]
            private string _shi2_passwd;

            public string shi2_passwd
            {
                get { return this._shi2_passwd; }
                set { this._shi2_passwd = value; }
            }
        }

        [DllImport("Netapi32", CharSet = CharSet.Auto)]
        public static extern int NetShareGetInfo([MarshalAs(UnmanagedType.LPWStr)] string servername, [MarshalAs(UnmanagedType.LPWStr)] string netname, int level, ref IntPtr bufptr);

        [DllImport("Netapi32", CharSet = CharSet.Auto)]
        internal static extern int NetApiBufferFree(IntPtr Buffer);

        #endregion

        #region -- MPR.DLL--

        [StructLayout(LayoutKind.Sequential)]
        public struct NetResourceW
        {
            private int _dwScope;

            public int dwScope
            {
                get { return this._dwScope; }
                set { this._dwScope = value; }
            }
            private int _dwType;

            public int dwType
            {
                get { return this._dwType; }
                set { this._dwType = value; }
            }
            private int _dwDisplayType;

            public int dwDisplayType
            {
                get { return this._dwDisplayType; }
                set { this._dwDisplayType = value; }
            }
            private int _dwUsage;

            public int dwUsage
            {
                get { return this._dwUsage; }
                set { this._dwUsage = value; }
            }
            [MarshalAs(UnmanagedType.LPWStr)]
            private string _lpLocalName;

            public string lpLocalName
            {
                get { return this._lpLocalName; }
                set { this._lpLocalName = value; }
            }
            [MarshalAs(UnmanagedType.LPWStr)]
            private string _lpRemoteName;

            public string lpRemoteName
            {
                get { return this._lpRemoteName; }
                set { this._lpRemoteName = value; }
            }
            [MarshalAs(UnmanagedType.LPWStr)]
            private string _lpComment;

            public string lpComment
            {
                get { return this._lpComment; }
                set { this._lpComment = value; }
            }
            [MarshalAs(UnmanagedType.LPWStr)]
            private string _lpProvider;

            public string lpProvider
            {
                get { return this._lpProvider; }
                set { this._lpProvider = value; }
            }
        }

        [DllImport("mpr.dll")]
        public static extern int WNetAddConnection2W([MarshalAs(UnmanagedType.LPArray)] NetResourceW[] lpNetResource, [MarshalAs(UnmanagedType.LPWStr)] string lpPassword, [MarshalAs(UnmanagedType.LPWStr)] string UserName, int dwFlags);

        [DllImport("mpr.dll")]
        public static extern int WNetCancelConnection2(string lpName, int dwFlags, bool fForce);

        #endregion

        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        public extern static UInt32 FindMimeFromData(
            UInt32 pBC,
            [MarshalAs(UnmanagedType.LPStr)] String pwzUrl,
            [MarshalAs(UnmanagedType.LPArray)] Byte[] pBuffer,
            UInt32 cbSize,
            [MarshalAs(UnmanagedType.LPStr)] String pwzMimeProposed,
            UInt32 dwMimeFlags,
            out UInt32 ppwzMimeOut,
            UInt32 dwReserverd
        );
    }
}