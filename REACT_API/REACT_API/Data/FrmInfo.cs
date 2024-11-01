using REACT_API.OverLayModels;

namespace REACT_API.Data
{
    public class FrmInfo
    {
        public void ProcessBulkCheck(BulkCheckRequest request)
        {
            CheckSimLock(request);
        }


        public string CheckSimLock(BulkCheckRequest request)
        {
            // Implementation logic here
            return $"{request.Imei}, {request.Rights}, {request.Id}, {request.Service}, {request.Service}";
        }
    }
}
