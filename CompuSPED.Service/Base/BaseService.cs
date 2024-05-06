using CompuSPED.Common.Base;
using CompuSPED.Common.Exceptions;
using CompuSPED.Data;
using System;
using System.Threading.Tasks;

namespace CompuSPED.Service.Base
{
    public class BaseService
    {
        public readonly string baseUri = "https://gg4l-sandbox2.lumentouchhosts.com/ims/oneroster/v1p1";
        private readonly DatabaseContext _context;
        public BaseService()
        {
            _context = new DatabaseContext();
        }
        public void ValidateDatabaseEntity<T>(T databaseEntity)
        {
            if (databaseEntity == null)
            {
                throw new AppValidationException("Record not found");
            }
        }

        public void CheckIfExist<T>(T databaseEntity)
        {
            if (databaseEntity != null)
            {
                throw new AppValidationException("Record found");
            }
        }

        public void CheckIfNotExist<T>(T databaseEntity)
        {
            if (databaseEntity == null)
            {
                throw new AppValidationException("Record not found");
            }
        }

        public void ThrowError(string message)
        {
            throw new AppValidationException(message);
        }

        public async Task<ServiceResponse<TResult>> ExecuteAsync<TResult>(Func<Task<TResult>> func, int errorCode = 0)
        {
            var response = new ServiceResponse<TResult>();

            try
            {
                response.Result = await func.Invoke();
                response.ErrorCode = errorCode;
                response.HasError = false;
                response.Exception = null;
                response.ErrorMessage = null;
                _context.Logs.Add(new Data.Entities.ErrorLog { 
                    ErrorType = "No Error", 
                    Location = func.Method.Name,
                    RegisteredDate = DateTime.Now
                });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = GetErrorMessage(ex);
                response.ErrorCode = errorCode;
                response.Result = default(TResult);
                response.HasError = true;
                response.Exception = ex;

                if(ex is AppValidationException)
                {
                    _context.Logs.Add(new Data.Entities.ErrorLog { 
                        ErrorType = "Application Error", 
                        Location = func.Method.Name, 
                        ErrorMessage =  response.ErrorMessage,
                        RegisteredDate = DateTime.Now
                    });
                }
                else
                {
                    _context.Logs.Add(new Data.Entities.ErrorLog { 
                        ErrorType = "Hard Error", 
                        Location = func.Method.Name,
                        ErrorMessage = ex.Message, 
                        StackTrace = ex.StackTrace, 
                        InnerException = ex.InnerException != null ? ex.InnerException.Message: "" ,
                        RegisteredDate = DateTime.Now
                    });
                }
                
                _context.SaveChanges();
            }

            return response;
        }

        private string GetErrorMessage(Exception ex)
        {
            if (ex is AppValidationException)
            {
                return ex.Message;
            }
            else
            {
                return "Process error";
            }
        }
    }
}
