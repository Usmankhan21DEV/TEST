namespace REACT_API.OverLayModels
{
    public class HistoryRequest
    {
        public string? Imei { get; set; } // Nullable string for IMEI
        public string? From { get; set; } // Nullable string for From date
        public string? To { get; set; } // Nullable string for To date
        public int Start { get; set; } // Start index
        public int Length { get; set; } // Length for pagination
        public string? Service { get; set; } // Nullable string for Service
        public string? UserSeqNum { get; set; } // Nullable string for User Sequence Number
    }
    public class ImeiRequest
    {
        public List<string>? Imei { get; set; } // Nullable string for IMEI
        public string? Service { get; set; } // Nullable string for Service Number
        public string? VMOpt { get; set; } // Nullable string for Request Mode
    }
    public class BulkCheckRequest
    {
        public List<string> Imei { get; set; }
        public string Rights { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Service { get; set; }
    }

}
