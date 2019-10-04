using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.ModelsDTO;

namespace BadBroker.WebService.Validation
{
    public interface IModelValidator
    {
        bool Validate(InputDTO inputDTO);
    }
}
