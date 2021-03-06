﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace store_api_test.DataMapping
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="znode_multifront")]
	public partial class CatalogDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertZNodeCatalog(ZNodeCatalog instance);
    partial void UpdateZNodeCatalog(ZNodeCatalog instance);
    partial void DeleteZNodeCatalog(ZNodeCatalog instance);
    #endregion
		
		public CatalogDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["znode_multifrontConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CatalogDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CatalogDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CatalogDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CatalogDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<ZNodeCatalog> ZNodeCatalogs
		{
			get
			{
				return this.GetTable<ZNodeCatalog>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ZNodeCatalog")]
	public partial class ZNodeCatalog : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CatalogID;
		
		private string _Name;
		
		private bool _IsActive;
		
		private System.Nullable<int> _PortalID;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCatalogIDChanging(int value);
    partial void OnCatalogIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnIsActiveChanging(bool value);
    partial void OnIsActiveChanged();
    partial void OnPortalIDChanging(System.Nullable<int> value);
    partial void OnPortalIDChanged();
    #endregion
		
		public ZNodeCatalog()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CatalogID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int CatalogID
		{
			get
			{
				return this._CatalogID;
			}
			set
			{
				if ((this._CatalogID != value))
				{
					this.OnCatalogIDChanging(value);
					this.SendPropertyChanging();
					this._CatalogID = value;
					this.SendPropertyChanged("CatalogID");
					this.OnCatalogIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsActive", DbType="Bit NOT NULL")]
		public bool IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				if ((this._IsActive != value))
				{
					this.OnIsActiveChanging(value);
					this.SendPropertyChanging();
					this._IsActive = value;
					this.SendPropertyChanged("IsActive");
					this.OnIsActiveChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PortalID", DbType="Int")]
		public System.Nullable<int> PortalID
		{
			get
			{
				return this._PortalID;
			}
			set
			{
				if ((this._PortalID != value))
				{
					this.OnPortalIDChanging(value);
					this.SendPropertyChanging();
					this._PortalID = value;
					this.SendPropertyChanged("PortalID");
					this.OnPortalIDChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
