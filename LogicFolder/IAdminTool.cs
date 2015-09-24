using System.Windows.Forms;

namespace CommonLibrary.AdminFolder
{
    /// <summary>
    ///     Интерфейс для управлениями и настроек форм
    /// </summary>
    public interface IAdminTool
    {
        /// <summary>
        ///     Закрылась активная форма
        /// </summary>
        void CloseActivateForm(Form form);

        /// <summary>
        ///     Активная форма изменилась
        /// </summary>
        void ActivatedFormChanged();
    }
}