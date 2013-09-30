using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HanyangVoting.CodeReader
{
    class XpsPrinter
    {
        public double Dpi { get; set; }
        public double Inch { get; set; }
        public double A4Width { get; set; }
        public double A4Height { get; set; }

        public double MarginX { get; set; }
        public double MarginY { get; set; }

        public double PageSizeWidth { get; set; }
        public double PageSizeHeight { get; set; }

        public double PageWidth { get; set; }
        public double PageHeight { get; set; }

        public double Scale { get; set; }

        public double TicketWidth { get; set; }
        public double TicketHeight { get; set; }

        public double DefaultTicketWidth { get; set; }
        public double DefaultTicketHeight { get; set; }

        public TransformGroup TransformGroup { get; set; }
        public double Thickness { get; set; }

        public FixedDocument Document { get; set; }
        public FixedPage CurrentPage { get; set; }

        public int CurrentX { get; set; }
        public int CurrentY { get; set; }

        const int CountX = 2;
        const int CountY = 2;

        public bool Flag { get; set; }

        public XpsPrinter()
        {
            Scale = 0.925;

            Dpi = 96;
            Inch = 25.4;
            A4Width = 210;
            A4Height = 297;

            Thickness = 1.2;

            double inchWidth = A4Width / Inch;
            double inchHeight = A4Height / Inch;

            PageSizeWidth = PageWidth = inchWidth * Dpi;
            PageSizeHeight = PageHeight = inchHeight * Dpi;

            double marginX = PageWidth - PageWidth * Scale - Thickness * 1;
            double marginY = PageHeight - PageHeight * Scale - Thickness * 1;

            PageWidth *= Scale;
            PageHeight *= Scale;

            MarginX = marginX * 0.5;
            MarginY = marginY * 0.5;

            TicketWidth = PageWidth / 2;
            TicketHeight = PageHeight / 2;

            DefaultTicketWidth = 420;
            DefaultTicketHeight = 594;

            TransformGroup = new TransformGroup();
            TransformGroup.Children.Add(
                new ScaleTransform(TicketWidth / DefaultTicketWidth, TicketHeight / DefaultTicketHeight));

            Document = new FixedDocument();
            Document.DocumentPaginator.PageSize = new Size(PageSizeWidth, PageSizeHeight);

            NewPage();
        }

        public void NewPage()
        {
            if (CurrentPage != null)
            {
                if (Flag == false)
                {
                    var rectangle0 = new Rectangle();
                    rectangle0.Fill = new SolidColorBrush(Colors.Black);
                    rectangle0.Width = Thickness;
                    rectangle0.Height = PageHeight + Thickness * 1;
                    rectangle0.Margin = new Thickness(MarginX + PageWidth / 2, MarginY, 0, 0);
                    CurrentPage.Children.Add(rectangle0);

                    for (int i = 1; i <= 1; i++)
                    {
                        var rectangle1 = new Rectangle();
                        rectangle1.Fill = new SolidColorBrush(Colors.Black);
                        rectangle1.Width = PageWidth + Thickness;
                        rectangle1.Height = Thickness;
                        double top = (TicketHeight + Thickness) * i - Thickness;
                        rectangle1.Margin = new Thickness(MarginX, MarginY + top, 0, 0);
                        CurrentPage.Children.Add(rectangle1);
                    }
                }

                PageContent content = new PageContent();
                content.Width = PageSizeWidth;
                content.Height = PageSizeHeight;

                CurrentPage.Margin = new Thickness(0);
                CurrentPage.UpdateLayout();

                ((IAddChild)content).AddChild(CurrentPage);
                Document.Pages.Add(content);
            }

            CurrentPage = new FixedPage();
            CurrentPage.Width = PageSizeWidth;
            CurrentPage.Height = PageSizeHeight;

            CurrentX = CurrentY = 0;
        }

        public void Increment()
        {
            CurrentX++;
            if (CountX <= CurrentX)
            {
                CurrentX = 0;
                CurrentY++;

                if (CountY <= CurrentY)
                {
                    CurrentY = 0;
                    NewPage();
                }
            }
        }

        public void NewControl(UserControl control)
        {
            control.Width = DefaultTicketWidth;
            control.Height = DefaultTicketHeight;

            double x = (TicketWidth + Thickness) * CurrentX;
            double y = (TicketHeight + Thickness) * CurrentY;

            control.Margin = new Thickness(MarginX + x, MarginY + y, 0, 0);
            control.Padding = new Thickness(0);

            CurrentPage.Children.Add(control);

            Increment();
        }

        public void NewTicket(HanyangVoting.Models.Ticket ticketData)
        {
            Ticket ticket = new Ticket();
            var bitmap = CodeWriter.GetQRCode(ticketData);
            ticket.image1.Source = bitmap.ToBitmapImage();
            bitmap.Dispose();

            ticket.textBlockKey.Text = ticketData.Key;
            ticket.textBlockName.Text = ticketData.Name;
            ticket.LayoutTransform = TransformGroup;

            NewControl(ticket);
        }

        public void Print()
        {
            PrintDialog dialog = new PrintDialog();
            if ((bool)dialog.ShowDialog().GetValueOrDefault())
            {
                dialog.PrintDocument(Document.DocumentPaginator, "HanyangVoting");
            }
        }
    }
}
