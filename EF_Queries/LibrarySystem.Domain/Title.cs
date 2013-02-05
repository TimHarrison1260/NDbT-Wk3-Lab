using System.Collections.Generic;

namespace LibrarySystem.Domain
{
    public class Title
    {
        public virtual int TitleId { get; set; }
        public virtual string TitleName { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string Author { get; set; }
        public virtual string ISBN { get; set; }
        public virtual Shelf Shelf { get; set; }
        public virtual List<Copy> Copies { get; set; }

        /// <summary>
        /// gets the first available copy of the title
        /// </summary>
        /// <returns>the first available copy</returns>
        public Copy GetCopy()
        {
            Copy copy = null;
            int i = 0;
            bool found = false;
            while (i < Copies.Count && found == false)
            {
                if (Copies[i].IsAvailable)
                {
                    found = true;
                    copy = Copies[i];
                }
                i++;
            }
            return copy;
        }
    }
}
