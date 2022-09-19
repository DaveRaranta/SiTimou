using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace gov.minahasa.sitimou.Helper
{
	internal class RegistryCore
	{
		#region === Registry Core ===

		public string SubKey { get; set; } // = "SOFTWARE\\" + Application.ProductName;

		public RegistryKey BaseRegistryKey { get; set; } = Registry.CurrentUser; //.LocalMachine;

		// private string _subKey = $"SOFTWARE\\{Application.ProductName}\\" + Application.ProductName + "\\{SubKey}";


		public bool Write(string keyName, object value, RegistryValueKind valueKind)
		{
			try
			{
				var rk = BaseRegistryKey;
				var sk1 = rk.CreateSubKey(SubKey);

				sk1?.SetValue(keyName, value, valueKind);

				return true;
			}
			catch (Exception ex)
			{
				// AppHelper.ShowError("RegistryTools", MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}
		}

		public object Read(string keyName)
		{
			var rk = BaseRegistryKey;
			var sk1 = rk.OpenSubKey(SubKey);

			if (sk1 == null)
			{
				return null;
			}
			else
			{
				try
				{
					return sk1.GetValue(keyName);
				}
				catch (Exception ex)
				{
					// AppHelper.ShowError("RegistryTools", MethodBase.GetCurrentMethod().Name, ex);
					return null;
				}
			}
		}

		public bool DeleteKey(string keyName)
		{
			try
			{
				var rk = BaseRegistryKey;
				var sk1 = rk.CreateSubKey(SubKey);

				if (sk1 == null)
					return true;
				else
					sk1.DeleteValue(keyName);

				return true;
			}
			catch (Exception ex)
			{
				// AppHelper.ShowError("RegistryTools", MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}
		}

		public bool DeleteSubKeyTree()
		{
			try
			{
				var rk = BaseRegistryKey;
				var sk1 = rk.OpenSubKey(SubKey);

				if (sk1 != null)
					rk.DeleteSubKeyTree(SubKey);

				return true;
			}
			catch (Exception ex)
			{
				// AppHelper.ShowError("RegistryTools", MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}
		}

		public int SubKeyCount()
		{
			try
			{
				var rk = BaseRegistryKey;
				var sk1 = rk.OpenSubKey(SubKey);

				if (sk1 != null)
					return sk1.SubKeyCount;
				else
					return 0;
			}
			catch (Exception ex)
			{
				// AppHelper.ShowError("RegistryTools", MethodBase.GetCurrentMethod().Name, ex);
				return 0;
			}
		}

		public int ValueCount()
		{
			try
			{
				var rk = BaseRegistryKey;
				var sk1 = rk.OpenSubKey(SubKey);

				if (sk1 != null)
					return sk1.ValueCount;
				else
					return 0;
			}
			catch (Exception ex)
			{
				// AppHelper.ShowError("RegistryTools", MethodBase.GetCurrentMethod().Name, ex);
				return 0;
			}
		}

		#endregion

	}

	internal class RegistryHelper
	{
		public static string ReadSettings(string key, string subkey = null)
		{
			var registry = new RegistryCore
			{
                SubKey = subkey == null ? "SOFTWARE\\Minahasa\\" + Application.ProductName : $"SOFTWARE\\Minahasa\\{Application.ProductName}\\{subkey}"
			};

			return (string)registry.Read(key);
		}

		public static void SaveSettings(string key, string value, string subkey = null)
		{
			var registry = new RegistryCore
			{
				SubKey = subkey == null ? "SOFTWARE\\Minahasa\\" + Application.ProductName : $"SOFTWARE\\Minahasa\\{Application.ProductName}\\{subkey}"
			};

			registry.Write(key, value, RegistryValueKind.String);

		}

	}

}
