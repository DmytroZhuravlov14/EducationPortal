using EducationPortal.Data;
using EducationPortal.Data.DTOs;

namespace EducationPortal.Services.Interfaces
{
    public interface IUserInfoInputService
    {
        User UserInfoInput();
        UserDTO UserDTOInfoInput();
        void PrintSessionUser();
    }
}
