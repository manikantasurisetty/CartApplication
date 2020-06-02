using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Configuration;
using Deloitte.ACE.Core.ConfigurationWrapper;
using System.Linq;
using System.Collections;
using Deloitte.ACE.Domain.Generic;
using Deloitte.ACE.Generic.DataLayer.Interfaces;
using Deloitte.ACE.Common;
using Deloitte.ACE.Domain.AirFare;
using Deloitte.ACE.Domain.Generic.Audit;
using Deloitte.ACE.Domain.GPS;
using Deloitte.ACE.Domain.Generic.Reference_Entities;
using QueryEngine.Model;
using Deloitte.ACE.DataLayer;
using Deloitte.ACE.Core;

namespace Deloitte.ACE.Generic.DataLayer
{
    public class ReferenceDL : BaseDL, IReferenceDL
    {
        private IDatabaseClientFactory _dbFactory;
        private IConfigurationWrapper _configuration;
        
        public ReferenceDL(IDatabaseClientFactory dbFactory, IConfigurationWrapper configuration)
        {
            _dbFactory = dbFactory;
            _configuration = configuration;
        }

        public ReferenceDL()
        {

        }

        public List<Reference_Mailbox> GetMailboxDetails()
        {
            const string StoredProcedure = "Reference_GetMasterData";
            List<Reference_Mailbox> objGetMailboxDetails = new List<Reference_Mailbox>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_Mailbox);

                DataSet mailBoxData = objDB.ExecuteDataSet(objCMD);
                dt = mailBoxData.Tables[0];


                objGetMailboxDetails = (from DataRow dr in dt.Rows
                                        select new Reference_Mailbox()
                                        {
                                            Id = Convert.ToInt16(dr["Id"]),
                                            Mailbox = dr["Mailbox"].ToString(),
                                            Reference_Process_Id = new Guid(dr["Reference_Process_Id"].ToString()),
                                            IsActive = Convert.ToBoolean(dr["IsActive"]),
                                            CreatedBy = dr["CreatedBy"].ToString(),
                                            CreatedDate = !string.IsNullOrWhiteSpace(dr["CreatedDate"].ToString()) ? Convert.ToDateTime(dr["CreatedDate"]) : (DateTime?)null,
                                            ModifiedBy = dr["ModifiedBy"].ToString(),
                                            ModifiedDate = !string.IsNullOrWhiteSpace(dr["ModifiedDate"].ToString()) ? Convert.ToDateTime(dr["ModifiedDate"]) : (DateTime?)null


                                        }).ToList();
                return objGetMailboxDetails;
            }
        }

        public DataSet FileManagementProcessTriggerStatus(StoredProcedure objStoredProcedure, Guid processId)
        {
            DataSet dsResult = null;
            if (objStoredProcedure!=null)
            {

                string connectionString = _configurationWrapper.GetConnectionStringBasedOnProcess(processId);
                dsResult = DBHelper.ExecuteProcedure(objStoredProcedure, connectionString);

            }
            return dsResult;
        }

        public List<Reference_Process> GetProcessDetails()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_Process> objGetProcessDetails = new List<Reference_Process>();

            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_Process);

                DataSet processData = objDB.ExecuteDataSet(objCMD);
                dt = processData.Tables[0];
                List<SelectListItem> lstProcessData = new List<SelectListItem>();

                objGetProcessDetails = (from DataRow dr in dt.Rows
                                        select new Reference_Process()
                                        {
                                            Id = new Guid(dr["Id"].ToString()),
                                            DefaultTemplate = dr["DefaultTemplate"].ToString(),
                                            Description = dr["Description"].ToString(),
                                            Process = dr["Process"].ToString(),
                                            IsActive = Convert.ToBoolean(dr["IsActive"]),
                                            CreatedBy = dr["CreatedBy"].ToString(),
                                            CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                            // ModifiedBy = dr["ModifiedBy"].ToString(),
                                            // ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"])


                                        }).ToList();

                return objGetProcessDetails;
            }
        }

        public List<Reference_Process> GetAllProcesses()
        {
            const string StoredProcedure = SPConstants.GetUAMData;
            List<Reference_Process> objGetProcessDetails = new List<Reference_Process>();

            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_Process);

                DataSet processData = objDB.ExecuteDataSet(objCMD);
                dt = processData.Tables[0];
                List<SelectListItem> lstProcessData = new List<SelectListItem>();

                objGetProcessDetails = (from DataRow dr in dt.Rows
                                        select new Reference_Process()
                                        {
                                            Id = new Guid(dr["Id"].ToString()),
                                            DefaultTemplate = dr["DefaultTemplate"].ToString(),
                                            Description = dr["Description"].ToString(),
                                            Process = dr["Process"].ToString(),
                                            IsActive = Convert.ToBoolean(dr["IsActive"]),
                                            CreatedBy = dr["CreatedBy"].ToString(),
                                            CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                            // ModifiedBy = dr["ModifiedBy"].ToString(),
                                            // ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"])


                                        }).ToList();

                return objGetProcessDetails;
            }
        }
        public List<Reference_ExternalLinks> GetExternalLinks()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_ExternalLinks> lstExternalLinks = new List<Reference_ExternalLinks>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {
                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_ExternalLinks);
                DataSet externalLinksData = objDB.ExecuteDataSet(objCMD);
                DataTable tblExternalLinks = externalLinksData.Tables[0];



                foreach (DataRow item in tblExternalLinks.Rows)
                {

                    Reference_ExternalLinks objExternalLinks = new Reference_ExternalLinks();
                    objExternalLinks.LinkName = item["LinkName"].ToString();
                    objExternalLinks.URL = item["URL"].ToString();
                    lstExternalLinks.Add(objExternalLinks);

                    //lstExternalLinks = null;
                    objExternalLinks = null;
                }

                return lstExternalLinks;
            }
        }
        public List<Reference_Role> GetRoleDetails()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_Role> objGetRoleDetails = new List<Reference_Role>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_Role);
                DataSet rolesData = objDB.ExecuteDataSet(objCMD);
                dt = rolesData.Tables[0];
                objGetRoleDetails = (from DataRow dr in dt.Rows
                                     select new Reference_Role()
                                     {
                                         Id = Convert.ToInt16(dr["Id"]),
                                         RoleName = dr["RoleName"].ToString(),
                                         Description = dr["Description"].ToString(),
                                         IsActive = Convert.ToBoolean(dr["IsActive"]),
                                         CreatedBy = dr["CreatedBy"].ToString(),
                                         CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                         ModifiedBy = dr["ModifiedBy"].ToString(),
                                         ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"])


                                     }).ToList();
                return objGetRoleDetails;
            }
        }
        public List<Reference_EmailType> GetEmailTypes()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_EmailType> objGetEmailTypes = new List<Reference_EmailType>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {
                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_EmailType);
                DataSet emailTypesData = objDB.ExecuteDataSet(objCMD);
                dt = emailTypesData.Tables[0];
                objGetEmailTypes = (from DataRow dr in dt.Rows
                                    select new Reference_EmailType()
                                    {
                                        Id = Convert.ToInt16(dr["Id"]),
                                        EmailType = dr["EmailType"].ToString(),
                                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                                        CreatedBy = dr["CreatedBy"].ToString(),
                                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                        ModifiedBy = Convert.ToString(dr["ModifiedBy"].ToString())


                                    }).ToList();
                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_EmailType);
                return objGetEmailTypes;
            }
        }

        public List<Reference_AuditStatus> GetAuditStatus()
        {
            const string StoredProcedure = "Reference_GetMasterTables";
            List<Reference_AuditStatus> objGetAuditStatus = new List<Reference_AuditStatus>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_AuditStatus);

                DataSet auditStatusData = objDB.ExecuteDataSet(objCMD);
                dt = auditStatusData.Tables[0];
                objGetAuditStatus = (from DataRow dr in dt.Rows
                                     select new Reference_AuditStatus()
                                     {
                                         Id = Convert.ToInt16(dr["Id"]),
                                         Status = dr["Status"].ToString(),
                                         IsActive = Convert.ToBoolean(dr["IsActive"]),
                                         CreatedBy = dr["CreatedBy"].ToString(),
                                         CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                         ModifiedBy = dr["ModifiedBy"].ToString(),
                                         ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"])


                                     }).ToList();
                return objGetAuditStatus;
            }
        }
        public List<SelectListItem> GetManagerAuditStatus()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;

            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();
            List<SelectListItem> lstAuditStatus = new List<SelectListItem>();
            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Refrence_ManagerAuditStatus);


                DataSet auditStatusData = objDB.ExecuteDataSet(objCMD);

                dt = auditStatusData.Tables[0];
                // List<SelectListItem> lstAuditorStatus = new List<SelectListItem>();
                foreach (DataRow item in dt.Rows)
                {
                    SelectListItem objAuditStatus = new SelectListItem();
                    objAuditStatus.Text = item["Status"].ToString();

                    objAuditStatus.Value = Convert.ToInt32(item["Id"]).ToString();

                    objAuditStatus.ColorCode = item["ColorCode"].ToString();
                    lstAuditStatus.Add(objAuditStatus);
                    objAuditStatus = null;
                }
                return lstAuditStatus;
            }

        }

        /// <summary>
        /// Get the manager audit status based on procedure.
        /// </summary>
        /// <param name="procedure">procedure</param>
        /// <returns>DataSet</returns>
        public DataSet GetManagerAuditStatus_New(StoredProcedure procedure, Nullable<Guid> processId = null)
        {
            DataSet dsResult = new DataSet();

            //string connectionString = _configurationWrapper.GetConnectionString("DataConnection").ConnectionString;
            string connectionString = _configurationWrapper.GetConnectionStringBasedOnProcess(processId.Value);
            Parameter paramProcessId = procedure.Parameters.Find(param => param.Name == "ProcessId");
            if (paramProcessId != null)
                paramProcessId.Value = processId;
            if (procedure != null)
            {
                dsResult = DBHelper.ExecuteProcedure(procedure, connectionString);
            }

            return dsResult;
        }

        /// <summary>
        /// Get the fiscal year based on procedure.
        /// </summary>
        /// <param name="procedure">procedure</param>
        /// <returns>DataSet</returns>
        public DataSet GetFiscalYear(StoredProcedure procedure)
        {
            DataSet dsResult = new DataSet();

            string connectionString = _configurationWrapper.GetConnectionString("DataConnection").ConnectionString;

            if (procedure != null)
            {
                dsResult = DBHelper.ExecuteProcedure(procedure, connectionString);
            }

            return dsResult;
        }

        public DataSet GetManagerAuditStatus_New(Guid processId, StoredProcedure procedure)
        {
            DataSet dsResult = new DataSet();

            string connectionString = _configurationWrapper.GetConnectionString("DataConnection").ConnectionString;

            if (procedure != null)
            {
                Parameter paramProcessId = procedure.Parameters.Find(param => param.Name == "ProcessId");

                if (paramProcessId != null)
                    paramProcessId.Value = processId;

                dsResult = DBHelper.ExecuteProcedure(procedure, connectionString);
            }

            return dsResult;
        }

        public List<SelectListItem> GetAuditorStatus(Guid AuditorId,Guid ProcessID)
        {
            string StoredProcedure = string.Empty;
            string ConnectionString = string.Empty;
            if (ProcessID != null && (ProcessID.ToString().ToLower() == Process.GPS.GetDescription().ToLower()))
            {
                StoredProcedure = SPConstants.GetMasterTables;
                ConnectionString = _configuration.GetConnectionStringGPS();
            }
            else if (ProcessID != null && (ProcessID.ToString().ToLower() == Process.DEA.GetDescription().ToLower()))
            {
                StoredProcedure = SPConstants_DEA.GetMasterTablesDEA;
                ConnectionString = _configuration.GetConnectionStringDEA();
            }
            else if ((ProcessID != null && (ProcessID.ToString().ToLower() == Process.AFRUS.GetDescription().ToLower())) ||
                (ProcessID != null && (ProcessID.ToString().ToLower() == Process.AFRUSI.GetDescription().ToLower())) ||
                (ProcessID != null && (ProcessID.ToString().ToLower() == Process.AFROTHER.GetDescription().ToLower())))
            {
                StoredProcedure = SPConstants_AFR.GetMasterTables;
                ConnectionString = _configuration.GetConnectionStringAFR();
            }

            List<Reference_AuditorStatus> objGetAuditorStatus = new List<Reference_AuditorStatus>();
            
            Database objDB = new SqlDatabase(ConnectionString);
            DataTable dt = new DataTable();
            List<SelectListItem> lstAuditorStatus = new List<SelectListItem>();
            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Refrence_AuditorStatus);
                objDB.AddInParameter(objCMD, "@AuditorId", DbType.Guid, AuditorId);
                objDB.AddInParameter(objCMD, "@ReferenceProcessId", DbType.Guid, ProcessID);
                DataSet auditStatusData = objDB.ExecuteDataSet(objCMD);
                DataTable auditorStatus = auditStatusData.Tables[1];
                string status = "";
                if (auditorStatus.Rows.Count > 0)
                {
                    status = auditorStatus.Rows[0].ItemArray[0].ToString();
                }
                dt = auditStatusData.Tables[0];
                // List<SelectListItem> lstAuditorStatus = new List<SelectListItem>();
                foreach (DataRow item in dt.Rows)
                {
                    SelectListItem objAuditorStatus = new SelectListItem();
                    objAuditorStatus.Text = item["Status"].ToString();

                    objAuditorStatus.Value = Convert.ToInt32(item["Id"]).ToString();
                    if (status != "" && item["Status"].ToString() == status)
                    { objAuditorStatus.IsSelected = true; }
                    else if (status == "" && objAuditorStatus.Text == "Available")
                    {
                        objAuditorStatus.IsSelected = true;
                    }
                    else
                    {

                        objAuditorStatus.IsSelected = false;
                    }
                    objAuditorStatus.ColorCode = item["ColorCode"].ToString();
                    lstAuditorStatus.Add(objAuditorStatus);
                    objAuditorStatus = null;
                }



                //objGetAuditorStatus = (from DataRow dr in dt.Rows
                //                     select new Reference_AuditorStatus()
                //                     {
                //                         Id = Convert.ToInt16(dr["Id"]),
                //                         Status = dr["Status"].ToString(),
                //                         IsActive = Convert.ToBoolean(dr["IsActive"]),
                //                         CreatedBy = dr["CreatedBy"].ToString(),
                //                         CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                //                         ModifiedBy = dr["ModifiedBy"].ToString(),
                //                         ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"])


                //                     }).ToList();
                return lstAuditorStatus;
            }
        }

        public List<Reference_ExceptionType> GetExceptionTypeDetails()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_ExceptionType> objGetExceptionTypeDetails = new List<Reference_ExceptionType>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_ExceptionType);
                try
                {
                    DataSet rolesData = objDB.ExecuteDataSet(objCMD);
                    dt = rolesData.Tables[0];

                    objGetExceptionTypeDetails = (from DataRow dr in dt.Rows
                                                  select new Reference_ExceptionType()
                                                  {
                                                      Id = Convert.ToInt16(dr["Id"]),
                                                      ExceptionType = dr["ExceptionType"].ToString(),
                                                      Description = dr["Description"].ToString(),
                                                      IsActive = Convert.ToBoolean(dr["IsActive"]),
                                                      CreatedBy = dr["CreatedBy"].ToString(),
                                                      CreatedDate = !string.IsNullOrWhiteSpace(dr["CreatedDate"].ToString()) ? Convert.ToDateTime(dr["CreatedDate"]) : (DateTime?)null,
                                                      ModifiedBy = dr["ModifiedBy"].ToString(),
                                                      ModifiedDate = !string.IsNullOrWhiteSpace(dr["ModifiedDate"].ToString()) ? Convert.ToDateTime(dr["ModifiedDate"]) : (DateTime?)null
                                                  }).ToList();
                }
                catch (Exception ex)
                {
                    ExceptionLog objExceptionLog = new ExceptionLog();
                    objExceptionLog.Source = "ReferenceDL";
                    objExceptionLog.Location = "GetExceptionTypeDetails";
                    objExceptionLog.Description = ex.InnerException.ToString();
                    objExceptionLog.Exception = ex.Message;
                    objExceptionLog.StackTrace = ex.StackTrace;
                    objExceptionLog.CreatedBy = "";
                    ExceptionLogDL objExceptionLogDL = new ExceptionLogDL(_dbFactory, _configuration);
                    objExceptionLogDL.InsertExceptionLog(objExceptionLog);

                    throw ex;
                }
                return objGetExceptionTypeDetails;
            }
        }
        public List<Reference_ExceptionCategory> GetExceptionCategoryDetails()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_ExceptionCategory> objGetExceptionCategoryDetails = new List<Reference_ExceptionCategory>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_ExceptionCategory);
                DataSet rolesData = objDB.ExecuteDataSet(objCMD);
                dt = rolesData.Tables[0];

                objGetExceptionCategoryDetails = (from DataRow dr in dt.Rows
                                                  select new Reference_ExceptionCategory()
                                                  {
                                                      Id = Convert.ToInt16(dr["Id"]),
                                                      CategoryType = dr["CategoryType"].ToString(),
                                                      Description = dr["Description"].ToString(),
                                                      IsActive = Convert.ToBoolean(dr["IsActive"]),
                                                      CreatedBy = dr["CreatedBy"].ToString(),
                                                      CreatedDate = !string.IsNullOrWhiteSpace(dr["CreatedDate"].ToString()) ? Convert.ToDateTime(dr["CreatedDate"]) : (DateTime?)null,
                                                      ModifiedBy = dr["ModifiedBy"].ToString(),
                                                      ModifiedDate = !string.IsNullOrWhiteSpace(dr["ModifiedDate"].ToString()) ? Convert.ToDateTime(dr["ModifiedDate"]) : (DateTime?)null
                                                  }).ToList();
                return objGetExceptionCategoryDetails;
            }
        }

        public List<Reference_AuditReason> GetAuditReason()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_AuditReason> objGetAuditReason = new List<Reference_AuditReason>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {
                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_AuditReason);
                DataSet importFlags = objDB.ExecuteDataSet(objCMD);
                dt = importFlags.Tables[0];

                objGetAuditReason = (from DataRow dr in dt.Rows
                                     select new Reference_AuditReason()
                                     {
                                         AuditReason = dr["AuditReason"].ToString(),
                                         Text = dr["AuditReason"].ToString()
                                     }).ToList();
                return objGetAuditReason;
            }

        }

        public List<Reference_ProcessServices> GetProcessServices()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_ProcessServices> objGetProcessServices = new List<Reference_ProcessServices>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {
                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_ProcessServices);

                DataSet importFlags = objDB.ExecuteDataSet(objCMD);
                dt = importFlags.Tables[0];

                objGetProcessServices = (from DataRow dr in dt.Rows
                                         select new Reference_ProcessServices()
                                         {
                                             Id = Convert.ToInt16(dr["Id"]),
                                             Reference_Process_Id = dr["Reference_ProcessId"].ToString(),
                                             EnforceStopFlag = Convert.ToBoolean(dr["EnforceStop"]),
                                             IsActive = Convert.ToBoolean(dr["IsActive"]),
                                             CreatedBy = dr["CreatedBy"].ToString(),
                                             CreatedDate = !string.IsNullOrWhiteSpace(dr["CreatedDate"].ToString()) ? Convert.ToDateTime(dr["CreatedDate"]) : (DateTime?)null
                                         }).ToList();
                return objGetProcessServices;
            }
        }
        public List<ReassignAudits> GetTeamMembers(Guid ProcessId)
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Domain.Generic.Audit.ReassignAudits> objGetProcessServices = new List<Domain.Generic.Audit.ReassignAudits>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {
                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.GetMemberTeam);
                objDB.AddInParameter(objCMD, "@ReferenceProcessId", DbType.Guid, ProcessId);

                DataSet importFlags = objDB.ExecuteDataSet(objCMD);
                dt = importFlags.Tables[0];

                objGetProcessServices = (from DataRow dr in dt.Rows
                                         select new Domain.Generic.Audit.ReassignAudits()
                                         {
                                             MemberId = new Guid(Convert.ToString(dr["MemberId"])),
                                             Status = dr["Status"].ToString(),
                                             ColorCode = Convert.ToString(dr["ColorCode"]),
                                             AuditorName = Convert.ToString(dr["AuditorName"]),
                                             }).ToList();
                return objGetProcessServices;
            }
        }

        /// <summary>
        /// Get the team members based on process id and procedure
        /// </summary>
        /// <param name="processId">processId</param>
        /// <param name="procedure">procedure</param>
        /// <returns></returns>
        public DataSet GetTeamMembers_New(Guid processId, StoredProcedure procedure)
        {
            DataSet dsResult = new DataSet();

            string connectionString = _configurationWrapper.GetConnectionStringBasedOnProcess(processId);

            if (procedure != null)
            {
                Parameter paramProcessId = procedure.Parameters.Find(param => param.Name == "ProcessId");

                if (paramProcessId != null)
                    paramProcessId.Value = processId;

                dsResult = DBHelper.ExecuteProcedure(procedure, connectionString);
            }

            return dsResult;
        }

        public List<Reference_Process> GetExceptionProcess()
        {
            const string StoredProcedure = SPConstants.GetUAMData;
            List<Reference_Process> objGetProcessDetails = new List<Reference_Process>();

            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_Process);

                DataSet processData = objDB.ExecuteDataSet(objCMD);
                dt = processData.Tables[0];
                List<Reference_Process> lstProcessData = new List<Reference_Process>();

                objGetProcessDetails = (from DataRow dr in dt.Rows
                                        select new Reference_Process()
                                        {
                                            Id = new Guid(dr["Id"].ToString()),
                                            Description = dr["Description"].ToString(),
                                            Process = dr["Process"].ToString(),
                                            IsActive = Convert.ToBoolean(dr["IsActive"]),
                                            CreatedBy = dr["CreatedBy"].ToString(),
                                            CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                            // ModifiedBy = dr["ModifiedBy"].ToString(),
                                            // ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"])
                                        }).ToList();

                return objGetProcessDetails;
            }
        }
        public List<Reference_ExternalLinks> GetReportLinks(Guid ProcessId)
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_ExternalLinks> lstReportLinks = new List<Reference_ExternalLinks>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {
                objDB.AddInParameter(objCMD, "@ReferenceProcessId", DbType.Guid, ProcessId);
                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_ReportLinks);
                DataSet reportLinksData = objDB.ExecuteDataSet(objCMD);
                DataTable tblReportLinks = reportLinksData.Tables[0];



                foreach (DataRow item in tblReportLinks.Rows)
                {

                    Reference_ExternalLinks objReportLinks = new Reference_ExternalLinks();
                    objReportLinks.LinkName = item["LinkName"].ToString();
                    objReportLinks.URL = item["URL"].ToString();
                    lstReportLinks.Add(objReportLinks);

                    //lstExternalLinks = null;
                    objReportLinks = null;
                }

                return lstReportLinks;
            }
        }
        public List<BiweeklyYears> GetBiweeklyYears()
        {
            List<BiweeklyYears> objSearchAudits = new List<BiweeklyYears>();
            ConnectionStringSettings connectionStringSettings = _configuration.GetConnectionString("DataConnection");
            // string connectionStringGPS = _configuration.GetConnectionStringGPS();
            Database objDB = new SqlDatabase(connectionStringSettings.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(SPConstants_Generic.GetBiweeklyYears))
            {

                DataSet SearchAuditsData = objDB.ExecuteDataSet(objCMD);

                dt = SearchAuditsData.Tables[0];
                objSearchAudits = (from DataRow dr in dt.Rows
                                   select new BiweeklyYears()
                                   {
                                       Year = Convert.ToString(dr["year"]),
                                       BiweeklyCount = Convert.ToInt32(dr["BiweeklyCount"])
                                       //  CurrentBiweekly = Convert.ToInt32(dr["CurrentBiwekly"])

                                   }).ToList();
                dt = SearchAuditsData.Tables[1];
                string Biweeklyyear = Convert.ToString(dt.Rows[0].ItemArray[0]);
                int CurrentBiweekly = Convert.ToInt32(dt.Rows[0].ItemArray[1]);
                foreach (var item in objSearchAudits)
                {
                    if (item.Year == Biweeklyyear)
                    {
                        item.CurrentBiweekly = CurrentBiweekly;
                    }
                    else
                    {
                        item.CurrentBiweekly = 0;
                    }
                    if (CurrentBiweekly == 1)
                    {
                        item.BiweeklyPeriod = "BW 26/" + Convert.ToString(Convert.ToInt32(Biweeklyyear) - 1);
                    }
                    else
                    {
                        if (CurrentBiweekly > 9)
                            item.BiweeklyPeriod = "BW" + " " + (CurrentBiweekly - 1) + "/" + Biweeklyyear;
                        else
                            item.BiweeklyPeriod = "BW" + " " + "0" + (CurrentBiweekly - 1) + "/" + Biweeklyyear;

                    }
                }
                return objSearchAudits;
            }
            }


        public List<SelectListItem> GetBiWeeklyCalendar()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_BiweeklyCalendar> objGetBiweeklyDetails = new List<Reference_BiweeklyCalendar>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();
            List<SelectListItem> lstBiweeklyCalendar = new List<SelectListItem>();
            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_BiWeeklyDetails);

                DataSet auditStatusData = objDB.ExecuteDataSet(objCMD);
                dt = auditStatusData.Tables[0];

                foreach (DataRow item in dt.Rows)
                {
                    SelectListItem objBiweeklyCalendar = new SelectListItem();
                    objBiweeklyCalendar.Text = item["Description"].ToString();
                    objBiweeklyCalendar.Value = item["StartDate"].ToString() + '-' + item["EndDate"].ToString();
                    objBiweeklyCalendar.FiscalYear = Convert.ToString(item["FiscalYear"]);
                    objBiweeklyCalendar.Period = Convert.ToString(item["Period"]);
                    if ((DateTime.Now).Date >= Convert.ToDateTime(item["StartDate"]).Date & (DateTime.Now).Date <= Convert.ToDateTime(item["EndDate"]).Date)
                    {
                        objBiweeklyCalendar.IsFutureDate = true;
                        objBiweeklyCalendar.IsSelected = true;
                    }
                    if (Convert.ToDateTime(item["StartDate"]).Date >= (DateTime.Now).Date)
                    {
                        objBiweeklyCalendar.IsFutureDate = true;
                    }

                    //if((DateTime.Now).Date<)
                    if (objBiweeklyCalendar.IsFutureDate == false || objBiweeklyCalendar.IsSelected == true)
                    {
                        lstBiweeklyCalendar.Add(objBiweeklyCalendar);
                    }
                    objBiweeklyCalendar = null;
                }
                int lstCount = lstBiweeklyCalendar.Count();
                lstBiweeklyCalendar[1].IsSelected = true;
                lstBiweeklyCalendar[0].IsSelected = false;
                lstBiweeklyCalendar[0].IsFutureDate = true;
                lstBiweeklyCalendar[1].IsFutureDate = false;

                return lstBiweeklyCalendar;
            }
        }

        public List<Reference_Process> GetReferenceProcesses()
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_Process> objGetProcessDetails = new List<Reference_Process>();

            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            DataTable dt = new DataTable();

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {

                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.GetExceptionProcesses);

                DataSet processData = objDB.ExecuteDataSet(objCMD);

                if(processData.HasData())
                {
                    dt = processData.Tables[0];
                    List<SelectListItem> lstProcessData = new List<SelectListItem>();

                    objGetProcessDetails = (from DataRow dr in dt.Rows
                                            select new Reference_Process()
                                            {
                                                Id = new Guid(dr["Id"].ToString()),
                                                DefaultTemplate = dr.String("DefaultTemplate"),
                                                Description = dr.String("Description"),
                                                Process = dr.String("Process"),
                                                IsActive = dr.Bool("IsActive"),
                                                CreatedBy = dr.String("CreatedBy"),
                                                CreatedDate = dr.Date("CreatedDate"),
                                                IsEligibleForAutoAssignment = dr.Bool("IsEligibleForAutoAssignment")
                                            }).ToList();
                }
                

                return objGetProcessDetails;
            }
        }

        public List<Reference_DataSourcingTemplates> GetDataSourcingTemplates(Guid ProcessId)
        {
            const string StoredProcedure = SPConstants.GetMasterTables;
            List<Reference_DataSourcingTemplates> lstDataSourcingTemplate = new List<Reference_DataSourcingTemplates>();
            ConnectionStringSettings ConnectionString = _configuration.GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);

            using (DbCommand objCMD = objDB.GetStoredProcCommand(StoredProcedure))
            {
                objDB.AddInParameter(objCMD, "@ReferenceProcessId", DbType.Guid, ProcessId);
                objDB.AddInParameter(objCMD, "@type", DbType.Int32, Reference_GetAllData.Reference_DataSourcingTemplates);
                DataSet DataSourcingTemplatesData = objDB.ExecuteDataSet(objCMD);

                if(DataSourcingTemplatesData.HasData())
                {
                    DataTable tblDataSourcingTemplates = DataSourcingTemplatesData.Table(0);

                    foreach (DataRow item in tblDataSourcingTemplates.Rows)
                    {

                        Reference_DataSourcingTemplates objDataSourcingTemplates = new Reference_DataSourcingTemplates();

                        objDataSourcingTemplates.Id = item.Int("Id");
                        objDataSourcingTemplates.TemplateName = item.String("TemplateName");

                        lstDataSourcingTemplate.Add(objDataSourcingTemplates);

                        objDataSourcingTemplates = null;
                    }
                }
                

                return lstDataSourcingTemplate;
            }

        }

        /// <summary>
        /// Get the AuditDetail Links based on process id and procedure
        /// </summary>
        /// <param name="processId">processId</param>
        /// <param name="procedure">procedure</param>
        /// <returns></returns>
        public DataSet GetAuditDetailLinks(Guid processId, StoredProcedure procedure)
        {
            DataSet dsResult = new DataSet();

            string connectionString = _configurationWrapper.GetConnectionStringBasedOnProcess(processId);
            dsResult = DBHelper.ExecuteProcedure(procedure, connectionString);

            return dsResult;
        }

        /// <summary>
        /// Get the team notifications, notification types, audit status, auditor status and events data.
        /// </summary>
        /// <param name="objStoredProcedure">objStoredProcedure</param>
        /// <param name="processId">processId</param>
        /// <param name="memberId">memberId</param>
        /// <returns>DataSet</returns>
        public DataSet GetTeamNotificationTypes(StoredProcedure objStoredProcedure, Guid processId, Guid memberId)
        {
            DataSet result = new DataSet();
            if (objStoredProcedure != null)
            {
                string connectionString = _configurationWrapper.GetConnectionStringBasedOnProcess(processId);
                objStoredProcedure.Parameters.Find(param => param.Name == "ProcessId").SetValues(processId);
                objStoredProcedure.Parameters.Find(param => param.Name == "MemberId").SetValues(memberId);

                result = DBHelper.ExecuteProcedure(objStoredProcedure, connectionString);

            }
            return result;
        }

    }
}
