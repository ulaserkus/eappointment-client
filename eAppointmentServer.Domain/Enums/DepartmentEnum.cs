using Ardalis.SmartEnum;

namespace eAppointmentServer.Domain.Enums;


public sealed class DepartmentEnum : SmartEnum<DepartmentEnum>
{
    public static readonly DepartmentEnum KBB = new DepartmentEnum("KBB", 1);
    public static readonly DepartmentEnum DENTAL = new DepartmentEnum("DENTAL", 2);
    public static readonly DepartmentEnum INTERNAL_MEDICINE = new DepartmentEnum("INTERNAL_MEDICINE", 3);
    public static readonly DepartmentEnum OPHTHALMOLOGY = new DepartmentEnum("OPHTHALMOLOGY", 4);
    public static readonly DepartmentEnum ORTHOPEDICS = new DepartmentEnum("ORTHOPEDICS", 5);
    public static readonly DepartmentEnum PSYCHIATRY = new DepartmentEnum("PSYCHIATRY", 6);
    public static readonly DepartmentEnum UROLOGY = new DepartmentEnum("UROLOGY", 7);
    public static readonly DepartmentEnum NEUROLOGY = new DepartmentEnum("NEUROLOGY", 8);
    public static readonly DepartmentEnum GYNECOLOGY = new DepartmentEnum("GYNECOLOGY", 9);
    public static readonly DepartmentEnum CARDIOLOGY = new DepartmentEnum("CARDIOLOGY", 10);
    public static readonly DepartmentEnum DERMATOLOGY = new DepartmentEnum("DERMATOLOGY", 11);
    public static readonly DepartmentEnum ENT = new DepartmentEnum("ENT", 12);
    public static readonly DepartmentEnum PEDIATRICS = new DepartmentEnum("PEDIATRICS", 13);
    public static readonly DepartmentEnum EMERGENCY = new DepartmentEnum("EMERGENCY", 14);
    public static readonly DepartmentEnum PHYSIOTHERAPY = new DepartmentEnum("PHYSIOTHERAPY", 15);
    public static readonly DepartmentEnum RADIOLOGY = new DepartmentEnum("RADIOLOGY", 16);
    public static readonly DepartmentEnum ONCOLOGY = new DepartmentEnum("ONCOLOGY", 17);
    public static readonly DepartmentEnum GASTROENTEROLOGY = new DepartmentEnum("GASTROENTEROLOGY", 18);
    public static readonly DepartmentEnum ENDOCRINOLOGY = new DepartmentEnum("ENDOCRINOLOGY", 19);
    public static readonly DepartmentEnum NEPHROLOGY = new DepartmentEnum("NEPHROLOGY", 20);
    public static readonly DepartmentEnum RHEUMATOLOGY = new DepartmentEnum("RHEUMATOLOGY", 21);
    public static readonly DepartmentEnum ALLERGY = new DepartmentEnum("ALLERGY", 22);
    public static readonly DepartmentEnum INFECTIOUS_DISEASES = new DepartmentEnum("INFECTIOUS_DISEASES", 23);
    public static readonly DepartmentEnum ANESTHESIOLOGY = new DepartmentEnum("ANESTHESIOLOGY", 24);
    public static readonly DepartmentEnum SURGERY = new DepartmentEnum("SURGERY", 25);
    public static readonly DepartmentEnum PLASTIC_SURGERY = new DepartmentEnum("PLASTIC_SURGERY", 26);
    public static readonly DepartmentEnum VASCULAR_SURGERY = new DepartmentEnum("VASCULAR_SURGERY", 27);
    public static readonly DepartmentEnum THORACIC_SURGERY = new DepartmentEnum("THORACIC_SURGERY", 28);
    public static readonly DepartmentEnum NEUROSURGERY = new DepartmentEnum("NEUROSURGERY", 29);
    public static readonly DepartmentEnum TRANSFUSION_MEDICINE = new DepartmentEnum("TRANSFUSION_MEDICINE", 30);
    public static readonly DepartmentEnum PATHOLOGY = new DepartmentEnum("PATHOLOGY", 31);
    public static readonly DepartmentEnum DERMATOLOGIC_SURGERY = new DepartmentEnum("DERMATOLOGIC_SURGERY", 32);


    public DepartmentEnum(string name, int value) : base(name, value)
    {
    }
}