using Domain.Mappers;
using Domain.Models;
using REST.DTOs.Events;

namespace REST.Mappers
{
    public class EventMapper : 
        IMapper<Event, EventDto>,
        IMapper<CreateEventDTo, Event>
        
}