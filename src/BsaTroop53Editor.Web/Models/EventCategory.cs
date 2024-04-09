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

namespace BsaTroop53Editor.Web.Models;

public enum EventCategory
{
    Uncategorized,

    Outings,

    CampingTrips,

    CourtsOfHonor,

    TroopMeetings,

    PatrolMeetings,

    CommitteeMeetings,

    OtherMeetings,

    Deadlines,

    ServiceProjects,
}

public static class EventCategoryExtensions
{
    public static string ToHumanReadableString( this EventCategory eventCategory )
    {
        return eventCategory switch
        {
            EventCategory.Outings => "Outings",
            EventCategory.ServiceProjects => "Service Projects",
            EventCategory.CampingTrips => "Camping Trips",
            EventCategory.CourtsOfHonor => "Courts of Honor",
            EventCategory.TroopMeetings => "Troop Meetings",
            EventCategory.PatrolMeetings => "Patrol Meetings",
            EventCategory.CommitteeMeetings => "Committee Meetings",
            EventCategory.OtherMeetings => "Other Meetings",
            EventCategory.Deadlines => "Deadlines",
            _ => "Uncategorized"
        };
    }
}
