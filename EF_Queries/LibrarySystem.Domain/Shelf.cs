using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Domain
{
    public class Shelf
    {
        public virtual string ShelfCode { get; set; }
        public virtual string SiteName { get; set; }
        public virtual string Address { get; set; }
        public virtual string PostCode { get; set; }

        private static string UK_POST_PATTERN =
@"^(?<AREA>[A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]?)(?<SPACE> {1,2})(?<PROPERTY>[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)$";

        [NotMapped]
        public virtual string Area
        {
            get
            {
                string area = null;
                Regex ukPostRegex = new Regex(UK_POST_PATTERN, RegexOptions.Compiled);
                Match match = ukPostRegex.Match(PostCode.Trim());
                if (match.Success)
                {
                    area = match.Groups["AREA"].Value;
                }
                return area;
            }
        }

        [NotMapped]
        public virtual string Property
        {
            get
            {
                string property = null;
                Regex ukPostRegex = new Regex(UK_POST_PATTERN, RegexOptions.Compiled);
                Match match = ukPostRegex.Match(PostCode.Trim());
                if (match.Success)
                {
                    property = match.Groups["PROPERTY"].Value;
                }
                return property;
            }
        }
    }
}
