using InventorySystem.Services.Models;
using InventorySystem.ViewModels;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;



namespace InventorySystem.Services.Extensions
{

    public class PdfService
    {


        public static byte[] GenerateDeliveryPdf(User user, DeskLapModel deskLap, int id, string Type, bool isUsed, string Date)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));

                    page.Header().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                        });

                        table.Cell().PaddingBottom(20).AlignLeft().Image("C:\\Users\\Abdelrahman.khalaf\\Desktop\\InventorySystem\\wwwroot\\images\\aalogo.png");
                        table.Cell().Text("PC Delivery").FontSize(20).FontColor(Colors.Grey.Darken1).AlignCenter();
                        table.Cell().Text("IT Department").FontSize(25).Bold().FontColor(Colors.Black).AlignRight();

                    });
                    page.Content().Column(column =>
                    {

                        column.Spacing(5);


                        column.Item().Text("1.Time Frame").Bold().FontSize(12).FontColor(Colors.Blue.Darken3);
                        column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(4);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(4);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(4);
                                });

                                table.Cell().Background(Colors.Grey.Lighten1).Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text("Log Id").Bold().FontColor(Colors.Blue.Darken3).FontSize(10);
                                table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text(id + "").FontSize(10);
                                table.Cell().Background(Colors.Grey.Lighten1).Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text("Receive").Bold().FontColor(Colors.Blue.Darken3).FontSize(10);
                                table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text(Date).FontSize(10);
                                table.Cell().Background(Colors.Grey.Lighten1).Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text("Expected").Bold().FontColor(Colors.Blue.Darken3).FontSize(10);
                                table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text("90 M").FontSize(10);
                            });

                        // معلومات المستخدم
                        column.Item().Text("2.User Information").Bold().FontSize(12).FontColor(Colors.Blue.Darken3);
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(4);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(4);
                            });

                            AddTableRow(table, "Fingerprint ", user.Fingerprint ?? "N/A", "User Name ", user.FirstName ?? "N/A" + " "+user.LastName );
                            AddTableRow(table, "Email ", user.Email ?? "N/A", "Mobile ", user.Phone ?? "N/A");
                            AddTableRow(table, "Job Title ", user.JobTitle ?? "N/A", "Department ", user.Department.Name ?? "N/A");
                            AddTableRow(table, "Reporting to ", user.Manager?.Fingerprint ?? "N/A" + " " + user?.Manager?.LastName ?? "N/A", "Location ", user.Site.Name ?? "N/A");
                        });

                        // معلومات الكمبيوتر
                        column.Item().Text("3.Computer Information").Bold().FontSize(12).FontColor(Colors.Blue.Darken3);
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(4);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(4);
                            });

                            AddTableRow(table, "PC Type ", Type ?? "N/A", "Model ", deskLap.Brand + " " + deskLap.ModelVersion ?? "N/A");
                            AddTableRow(table, "PC Name ", deskLap.DeviceName ?? "N/A", "Condition ", isUsed ? "Used" : "New");
                            AddTableRow(table, "RAM ", deskLap.Ram ?? "N/A", "CPU ", deskLap.Cpu ?? "N/A");
                            AddTableRow(table, "GPU ", deskLap.Gpu ?? "N/A", "Hard Disk", deskLap.HardDisk ?? "N/A");
                            AddTableRow(table, "Wi-Fi MAC", deskLap.MacWifi ?? "N/A", "Ethernet MAC", deskLap.MacEthernet ?? "N/A");
                            AddTableRow(table, "OS", deskLap.OS, "Scrren Size", deskLap.ScreenSize ?? "N/A");

                        });

                        // الخطوات الفنية
                        column.Item().Text("4.Technical Steps").Bold().FontSize(12).FontColor(Colors.Blue.Darken3);
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                            });

                            string[] steps = { "☐ Windows Install", "☐ Windows", "☐ Send MAC", "☐ Office Installs", "☐ Email Setup", "☐ Drivers" };
                            string[] steps2 = { "☐ Label", "☐ OneDrive", "☐ Ang Desk", "☐ TrendMicro", "☐ Teams", "☐ WinRAR", "☐ PDF Application" };
                            string[] steps3 = { "☐ Printers Setup", "☐ Update", "☐ Chrome", "☐ Manage Engine", "☐ Klite Codec Media", "☐ Sophos Client" };

                            for (int i = 0; i < steps.Length; i++)
                            {
                                table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text(steps[i]).FontSize(10);
                                table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text(i < steps2.Length ? steps2[i] : "").FontSize(10);
                                table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text(i < steps3.Length ? steps3[i] : "").FontSize(10);
                            }
                        });

                        column.Item().Text("5.Notes").Bold().FontSize(12).FontColor(Colors.Blue.Darken3);
                        column.Item().Border(1).BorderColor(Colors.Grey.Darken1).Padding(10).Text(" ");
                        column.Item().Border(1).BorderColor(Colors.Grey.Darken1).Padding(10).Text("“I hereby acknowledge receipt of the computer along with its accessories, as detailed above. I commit to maintaining it and using it for work purposes only." +
                            " I also assume full responsibility in case of its theft, loss, or damage. Furthermore, I commit to returning the laptop device upon request from the administration.” ").FontSize(10);

                        column.Item().Text("6.Confirmations").Bold().FontSize(12).FontColor(Colors.Blue.Darken3);
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                            });

                            table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text("IT Support\n\n").Underline().Bold().FontColor(Colors.Blue.Darken3).AlignCenter();
                            table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text("Received By").Underline().Bold().FontColor(Colors.Blue.Darken3).AlignCenter();
                            table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text("Approve").Underline().Bold().FontColor(Colors.Blue.Darken3).AlignCenter();
                        });

                    });
                });
            });

            return document.GeneratePdf();
        }

        private static void AddTableRow(TableDescriptor table, string label1, string value1, string label2, string value2)
        {
            table.Cell().Background(Colors.Grey.Lighten1).Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text(label1).Bold().FontColor(Colors.Blue.Darken3).FontSize(10);
            table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text(value1).FontSize(10);
            table.Cell().Background(Colors.Grey.Lighten1).Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text(label2).Bold().FontColor(Colors.Blue.Darken3).FontSize(10);
            table.Cell().Border(1).BorderColor(Colors.Grey.Darken1).Padding(5).Text(value2).FontSize(10);
        }
    }
}



