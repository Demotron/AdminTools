namespace CommonLibrary
{
    public interface IActionButton
    {
        /// <summary>
        ///     Добавить новую строку в DataSource
        /// </summary>
        void AddNewRow();

        /// <summary>
        ///     Удалить строку из DataSource
        /// </summary>
        void DeleteRow();

        /// <summary>
        ///     Обновить данные из БД
        /// </summary>
        void RefreshData();

        /// <summary>
        ///     Сохранить изменения в БД
        /// </summary>
        bool SaveChanges(bool showMsg = true);

        /// <summary>
        ///     Сохранить таблицу в Excel
        /// </summary>
        void ExportToExcel();
    }
}