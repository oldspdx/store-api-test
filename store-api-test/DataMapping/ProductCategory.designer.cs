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
	public partial class ProductCategoryDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertZNodeProductCategory(ZNodeProductCategory instance);
    partial void UpdateZNodeProductCategory(ZNodeProductCategory instance);
    partial void DeleteZNodeProductCategory(ZNodeProductCategory instance);
    #endregion
		
		public ProductCategoryDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["znode_multifrontConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ProductCategoryDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ProductCategoryDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ProductCategoryDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ProductCategoryDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<ZNodeProductCategory> ZNodeProductCategories
		{
			get
			{
				return this.GetTable<ZNodeProductCategory>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ZNodeProductCategory")]
	public partial class ZNodeProductCategory : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ProductCategoryID;
		
		private int _ProductID;
		
		private string _Theme;
		
		private string _MasterPage;
		
		private string _CSS;
		
		private int _CategoryID;
		
		private System.Nullable<System.DateTime> _BeginDate;
		
		private System.Nullable<System.DateTime> _EndDate;
		
		private System.Nullable<int> _DisplayOrder;
		
		private bool _ActiveInd;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnProductCategoryIDChanging(int value);
    partial void OnProductCategoryIDChanged();
    partial void OnProductIDChanging(int value);
    partial void OnProductIDChanged();
    partial void OnThemeChanging(string value);
    partial void OnThemeChanged();
    partial void OnMasterPageChanging(string value);
    partial void OnMasterPageChanged();
    partial void OnCSSChanging(string value);
    partial void OnCSSChanged();
    partial void OnCategoryIDChanging(int value);
    partial void OnCategoryIDChanged();
    partial void OnBeginDateChanging(System.Nullable<System.DateTime> value);
    partial void OnBeginDateChanged();
    partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
    partial void OnEndDateChanged();
    partial void OnDisplayOrderChanging(System.Nullable<int> value);
    partial void OnDisplayOrderChanged();
    partial void OnActiveIndChanging(bool value);
    partial void OnActiveIndChanged();
    #endregion
		
		public ZNodeProductCategory()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductCategoryID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ProductCategoryID
		{
			get
			{
				return this._ProductCategoryID;
			}
			set
			{
				if ((this._ProductCategoryID != value))
				{
					this.OnProductCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._ProductCategoryID = value;
					this.SendPropertyChanged("ProductCategoryID");
					this.OnProductCategoryIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductID", DbType="Int NOT NULL")]
		public int ProductID
		{
			get
			{
				return this._ProductID;
			}
			set
			{
				if ((this._ProductID != value))
				{
					this.OnProductIDChanging(value);
					this.SendPropertyChanging();
					this._ProductID = value;
					this.SendPropertyChanged("ProductID");
					this.OnProductIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Theme", DbType="NVarChar(MAX)")]
		public string Theme
		{
			get
			{
				return this._Theme;
			}
			set
			{
				if ((this._Theme != value))
				{
					this.OnThemeChanging(value);
					this.SendPropertyChanging();
					this._Theme = value;
					this.SendPropertyChanged("Theme");
					this.OnThemeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MasterPage", DbType="NVarChar(MAX)")]
		public string MasterPage
		{
			get
			{
				return this._MasterPage;
			}
			set
			{
				if ((this._MasterPage != value))
				{
					this.OnMasterPageChanging(value);
					this.SendPropertyChanging();
					this._MasterPage = value;
					this.SendPropertyChanged("MasterPage");
					this.OnMasterPageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CSS", DbType="NVarChar(MAX)")]
		public string CSS
		{
			get
			{
				return this._CSS;
			}
			set
			{
				if ((this._CSS != value))
				{
					this.OnCSSChanging(value);
					this.SendPropertyChanging();
					this._CSS = value;
					this.SendPropertyChanged("CSS");
					this.OnCSSChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryID", DbType="Int NOT NULL")]
		public int CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if ((this._CategoryID != value))
				{
					this.OnCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._CategoryID = value;
					this.SendPropertyChanged("CategoryID");
					this.OnCategoryIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BeginDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> BeginDate
		{
			get
			{
				return this._BeginDate;
			}
			set
			{
				if ((this._BeginDate != value))
				{
					this.OnBeginDateChanging(value);
					this.SendPropertyChanging();
					this._BeginDate = value;
					this.SendPropertyChanged("BeginDate");
					this.OnBeginDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				if ((this._EndDate != value))
				{
					this.OnEndDateChanging(value);
					this.SendPropertyChanging();
					this._EndDate = value;
					this.SendPropertyChanged("EndDate");
					this.OnEndDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DisplayOrder", DbType="Int")]
		public System.Nullable<int> DisplayOrder
		{
			get
			{
				return this._DisplayOrder;
			}
			set
			{
				if ((this._DisplayOrder != value))
				{
					this.OnDisplayOrderChanging(value);
					this.SendPropertyChanging();
					this._DisplayOrder = value;
					this.SendPropertyChanged("DisplayOrder");
					this.OnDisplayOrderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActiveInd", DbType="Bit NOT NULL")]
		public bool ActiveInd
		{
			get
			{
				return this._ActiveInd;
			}
			set
			{
				if ((this._ActiveInd != value))
				{
					this.OnActiveIndChanging(value);
					this.SendPropertyChanging();
					this._ActiveInd = value;
					this.SendPropertyChanged("ActiveInd");
					this.OnActiveIndChanged();
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
