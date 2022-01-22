using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering.Models
{
    public class ResponseWithErrorMessage<T>
    {
        public ResponseWithErrorMessage(T response = default(T))
        {
            Response = response;
        }

        public ResponseWithErrorMessage(string errorMessage = "")
        {
            ErrorMessage = errorMessage;
        }

        public bool IsCorrect
        {
            get
            {
                return string.IsNullOrWhiteSpace(ErrorMessage);
            }
        }

        public T Response { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}
