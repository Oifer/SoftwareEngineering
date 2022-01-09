using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering.Models
{
    public class ResponseWithErrorMessage<T>
    {
        public ResponseWithErrorMessage(T response)
        {
            Response = response;
        }

        public ResponseWithErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public bool IsCorrect => string.IsNullOrWhiteSpace(ErrorMessage);

        public T Response { get; } = default;

        public string ErrorMessage { get; } = string.Empty;
    }
}
