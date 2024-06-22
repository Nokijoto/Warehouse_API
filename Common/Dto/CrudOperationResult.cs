using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class CrudOperationResult<TDto>
    {
        public CrudOperationResultStatus Status { get; set; }

        public TDto? Result { get; set; }
        public string Message { get; set; }
    }
}
