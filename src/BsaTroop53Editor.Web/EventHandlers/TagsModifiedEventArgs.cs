using Microsoft.AspNetCore.Components;

namespace BsaTroop53Editor.Web.EventHandlers
{
    [EventHandler( "ontagsmodified", typeof( TagsModifiedEventArgs ) )]
    public class TagsModifiedEventArgs
    {
        // ---------------- Constructor ----------------

        public TagsModifiedEventArgs( ICollection<string>? tags )
        {
            if( tags is null )
            {
                this.Tags = Array.Empty<string>();
            }
            else
            {
                this.Tags = tags;
            }
        }

        // ---------------- Properties ----------------

        public ICollection<string> Tags { get; }
    }
}
