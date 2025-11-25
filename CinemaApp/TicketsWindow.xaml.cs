using DatabaseLibrary.Contexts;
using DatabaseLibrary.Models;
using DatabaseLibrary.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaApp
{
    /// <summary>
    /// Логика взаимодействия для TicketsWindow.xaml
    /// </summary>
    public partial class TicketsWindow : Window
    {
        private readonly TicketService _service = new(new CinemaDbContext());
        public TicketsWindow()
        {
            InitializeComponent();
        }

        private async void TextExportButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveTicketInfoAsync("txt");
        }

        

        private async void PdfExportButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveTicketInfoAsync("pdf");
        }
        private async Task SaveTicketInfoAsync(string format)
        {
            int ticketId;
            if (!int.TryParse(TicketIdTextBox.Text, out ticketId))
            {
                MessageBox.Show("Некорректный номер билета", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Ticket ticket = await _service.GetTicketById(ticketId);
            if (ticket is null)
            {
                MessageBox.Show("Этого билета не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (format)
            {
                default:
                    MessageBox.Show("Не удалось экспортировать", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "txt":
                    await SaveTicketAsTxt(ticket);
                    break;
                case "pdf":
                    await SaveTicketAsPdf(ticket);
                    break;
            }
        }

        private async Task SaveTicketAsTxt(Ticket ticket)
        {
            var ticketDto = await _service.GetTicketInfoAsync(ticket);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый файл(*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == false)
                return;

            using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
            {
                await writer.WriteLineAsync($"Билет № {ticketDto.TicketId}");
                await writer.WriteLineAsync(ticketDto.FilmName);
                await writer.WriteLineAsync($"Начало сеанса: {ticketDto.StartSession}");
                await writer.WriteLineAsync($"Кинотеатр: {ticketDto.CinemaName}");
                await writer.WriteLineAsync($"Зал: {ticketDto.HallId}");
                await writer.WriteLineAsync($"Ряд: {ticketDto.Row} Место: {ticketDto.Seat}");
            }

            MessageBox.Show("Сохранено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async Task SaveTicketAsPdf(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
