//
// BsaTroop53 Editor - A way to create posts for bsatroop53.com
// Copyright (C) 2024 Seth Hendrick
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System.ComponentModel.DataAnnotations;
using Ical.Net.CalendarComponents;

namespace BsaTroop53Editor.Web.Models
{
    public class TroopEvent
    {
        // ---------------- Constructor ----------------

        public TroopEvent()
        {
        }

        public TroopEvent( CalendarEvent calendarEvent )
        {
            this.EventTitle = calendarEvent.Summary;
            this.StartTime = calendarEvent.Start.AsSystemLocal;
            this.EndTime = calendarEvent.End.AsSystemLocal;
            this.IsAllDayEvent = calendarEvent.IsAllDay;
            this.Location = calendarEvent.Location;
            this.Description = calendarEvent.Description;
            this.IsEditing = false;
        }

        // ---------------- Properties ----------------

        [Required]
        public string? EventTitle { get; set; } = null;

        [Required]
        public DateTime? StartTime { get; set; } = null;

        [Required]
        public DateTime? EndTime { get; set; } = null;

        public bool IsAllDayEvent { get; set; } = false;

        public string? Location { get; set; } = null;

        public string? Description { get; set; } = null;

        public bool IsEditing { get; set; } = false;
    }
}
