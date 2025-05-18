using eAppointmentServer.Domain.Entities;

namespace eAppointmentServer.Application;

public static class Constants
{
    public static readonly List<AppRole> Roles = [

        new AppRole
        {
            Id = Guid.Parse("ce4b80b8-4112-4012-8fc4-529c2d5c24a3"),
            Name = "Admin",
        },
         new AppRole
        {
            Id = Guid.Parse("667c023b-8401-4ce7-b041-4d840157d0fd"),
            Name = "Doctor",
        },

         new AppRole
        {
            Id = Guid.Parse("54fe26f3-e7fe-4367-9b29-050e513ea00d"),
            Name = "Staff",
        },
    ];
}