namespace Employee.Management.System.Exceptions
{
    public enum ErrorCode
    {
        None = 0,
        UnKnown = 1,

        // Common
        NotFound = 10,
        ValidationError = 11,
        Duplicated = 12,

        // Exam-specific
        ExamNotFound = 1000,
        NotValidExamDate = 1001,

        // Instructor-specific
        NotValidInstructorBirthDate = 2000,
        InternalserverError=400,
    }
}
