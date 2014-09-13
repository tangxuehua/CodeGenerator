
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Resources;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Xml;

using NetFocus.Components.AddIns;
using NetFocus.Components.GuiInterface.Services;

namespace NetFocus.UtilityTool.CodeGenerator.Services
{
	/// <summary>
	/// This class does basic service handling for you.
	/// </summary>
	public class ServiceManager
	{
		ArrayList serviceList       = new ArrayList();
		Hashtable servicesHashtable = new Hashtable();
		
		static ServiceManager defaultServiceManager = new ServiceManager();
		
		/// <summary>
		/// Gets the default ServiceManager
		/// </summary>
		public static ServiceManager Services {
			get {
				return defaultServiceManager;
			}
		}		
		
		/// <summary>
		/// Don't create ServiceManager objects, only have ONE per application.
		/// </summary>
		private ServiceManager()
		{

		}
		
		/// <remarks>
		/// This method initializes the service system to a path inside the add-in tree.
		/// This method must be called ONCE.
		/// </remarks>
		public void InitializeServicesSubsystem(string servicesPath)
		{
			// add add-in tree services
			AddServices((IService[])AddInTreeSingleton.AddInTree.GetTreeNode(servicesPath).BuildChildItems(this).ToArray(typeof(IService)));
			
			// initialize all services
			foreach (IService service in serviceList) {
				service.InitializeService();
			}
		}
		
		/// <remarks>
		/// Calls UnloadService on all services. This method must be called ONCE.
		/// </remarks>
		public void UnloadAllServices()
		{
			foreach (IService service in serviceList) {
				service.UnloadService();
			}
		}
		
		public void AddService(IService service)
		{
			serviceList.Add(service);
		}
		
		public void AddServices(IService[] services)
		{
			foreach (IService service in services) {
				AddService(service);
			}
		}
		
		// HACK: MONO BUGFIX
		// this doesn't work on mono:serviceType.IsInstanceOfType(service)
		bool IsInstanceOfType(Type type, IService service)
		{
			Type serviceType = service.GetType();

			foreach (Type iface in serviceType.GetInterfaces()) {
				if (iface == type) {
					return true;
				}
			}
			
			while (serviceType != typeof(System.Object)) {
				if (type == serviceType) {
					return true;
				}
				serviceType = serviceType.BaseType;
			}
			return false;
		}
		
		/// <remarks>
		/// Requestes a specific service, may return null if this service is not found.
		/// </remarks>
		public IService GetService(Type serviceType)
		{
			IService s = (IService)servicesHashtable[serviceType];
			if (s != null) {
				return s;
			}
			
			foreach (IService service in serviceList) {
				if (IsInstanceOfType(serviceType, service)) {
					servicesHashtable[serviceType] = service;
					return service;
				}
			}
			
			return null;
		}
	}
}
