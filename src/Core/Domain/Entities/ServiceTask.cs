﻿
using DAT154Oblig4.Domain.Enums;

namespace DAT154Oblig4.Domain.Entities
{
    public class ServiceTask
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public string Description { get; set; }
        public ServiceTaskType TaskType { get; set; }
        public ServiceTaskStatus TaskStatus { get; set; }
        public ServiceTaskPriority Priority { get; set; }
        public string Notes { get; set; }

        public ServiceTask() { }
        public ServiceTask(int roomId, ServiceTaskType taskType, string description, ServiceTaskStatus taskStatus, ServiceTaskPriority priority, string notes)
        {
            RoomId = roomId;
            TaskType = taskType;
            Description = description;
            TaskStatus = taskStatus;
            Priority = priority;
            Notes = notes;
        }
    }
}
