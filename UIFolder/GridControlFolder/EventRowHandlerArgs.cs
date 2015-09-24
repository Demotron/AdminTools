using System;

namespace CommonLibrary.UIFolder.GridControlFolder
{
    public class EventRowHandlerArgs : EventArgs
    {
        private readonly int rowHandler;

        public int RowHandler
        {
            get
            {
                return rowHandler;
            }
        }

        /// <summary>
        /// Отменить выполнение кода после события
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Создать новый класс для передачи в качестве параметра событию
        /// </summary>
        /// <param name="_rowHandler">Номер строки в таблице</param>
        public EventRowHandlerArgs(int _rowHandler)
        {
            rowHandler = _rowHandler;
        }
    }
}
