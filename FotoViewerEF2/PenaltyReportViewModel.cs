using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FotoViewerEF2
{
    public class PenaltyReportViewModel
    {
        public string SumUndoSpaceText { get; private set; }
        public string TotalSumText { get; private set; }
        public IEnumerable<KeyValuePair<int, int>> Penalties { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public PenaltyReportViewModel(PenaltyReport report)
        {
            // Рассчитываем результаты
            report.CalculationResult();

            // Устанавливаем текстовые поля
            SumUndoSpaceText = $"Сумма штрафа до пробела: {report.SumPenaltyUndoSpace}";
            TotalSumText = $"Общая сумма штрафа: {report.SumPenalty}";

            // Сортируем штрафы по ключу
            Penalties = report.PenaltyDictionary.OrderBy(kvp => kvp.Key);
        }
    }

    // Простая реализация RelayCommand
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged;
    }
}
