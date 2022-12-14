import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";
import {createEventId} from "./event-utils";
import FullCalendar from "@fullcalendar/react";
import React, {useEffect, useState} from "react";
import * as eventService from '../../services/eventService';



const Calendar = () => {

   const [events, setEvents] = useState([]);
   const eventApi = "https://localhost:7156/api/Event";

    useEffect(() => {

        const getEvents = async () => {

            const eventsData = await eventService.getAll();
            
            setEvents(eventsData);
            
        }

        getEvents();
        
    },[]);

    
    const handleEvents = (events) => {

       setEvents(events);

    }

    const handleDateSelect = (selectInfo) => {
        let title = prompt('Please enter a new title for your event')
        let calendarApi = selectInfo.view.calendar

        calendarApi.unselect() // clear date selection

        if (title) {
            calendarApi.addEvent({
                id: createEventId(),
                title,
                start: selectInfo.startStr,
                end: selectInfo.endStr,
                
            })
        }
    }

    const handleEventClick = async (clickInfo) => {
        if (window.confirm(`Are you sure you want to delete the event '${clickInfo.event.title}'`)) {
            
            await eventService.deleteById(clickInfo.event.toJSON().id);
                
            await clickInfo.event.remove()
        }
    }

    function renderEventContent(eventInfo) {
        return (
            <>
                <b>{eventInfo.timeText}</b>
                <i>{eventInfo.event.title}</i>
            </>
        )
    }

    const eventAddHandler = (eventToBeCreated) => {

       eventService.add(eventToBeCreated["event"])
           .then(result => {
               console.log(result);
           })
           .catch((error) => {
               console.log(error);
           });
    }

    return (
        <FullCalendar
            plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
            headerToolbar={{
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            }}
            initialView='dayGridMonth'
            height={900}
            editable={true}
            selectable={true}
            selectMirror={true}
            dayMaxEvents={true}
            weekends={true}
            events={`${eventApi}/getAll`}
            select={handleDateSelect}
            eventContent={renderEventContent} // custom render function
            eventClick={handleEventClick}
            eventsSet={handleEvents} // called after events are initialized/added/changed/removed
            eventAdd={eventAddHandler}
        />
    )

}

export default Calendar;