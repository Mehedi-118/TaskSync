using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using PTSL.Ovidhan.Common.Helper;

namespace PTSL.Ovidhan.Common.QuerySerialize.Implementation
{
    public class Pagination
    {
        private int _defaultPageNumber = 1;
        private int _defaultPageSize = 10;

        [JsonIgnore, SwaggerExclude]
        public int Start
        {
            get
            {
                return (PageNumber - 1) * PageSize;
            }
        }

        [JsonIgnore, SwaggerExclude]
        public int Limit
        {
            get
            {
                return PageSize;
            }
        }

        [Required]
        public int PageNumber
        {
            get
            {
                return _defaultPageNumber;
            }
            set
            {
                if (value <= 0) _defaultPageNumber = 1;
                else _defaultPageNumber = value;
            }
        }

        [Required]
        public int PageSize
        {
            get
            {
                return _defaultPageSize;
            }
            set
            {
                if (value <= 0) _defaultPageSize = 10;
                else _defaultPageSize = value;
            }
        }

    }
}