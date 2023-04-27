using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;


namespace PDFAnalyzerNamespace
{
    public class PDFAnalyzer
    {
        string filePath = "/Users/myav/Desktop/Test-Items/template_resume_test.pdf";
        public PdfContent ReadPdfContent()
        {
            PdfContent content = new PdfContent();
            using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(filePath)))
            {
                int numberOfPages = pdfDocument.GetNumberOfPages();
                for (int page = 1; page <= numberOfPages; page++)
                {
                    PdfPage pdfPage = pdfDocument.GetPage(page);
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(pdfPage, strategy);
                    string[] lines = text.Split('\n');
                    foreach (string line in lines)
                    {
                        if (line.Contains("Name:"))
                        {
                            content.Name = line.Replace("Name:", "").Trim();
                        }
                        else if (line.Contains("Education:"))
                        {
                            content.Education = line.Replace("Education:", "").Trim();
                        }
                        else if (line.Contains("Experience_1:"))
                        {
                            content.Experience1 = line.Replace("Experience_1:", "").Trim();
                        }
                        else if (line.Contains("Experience_2:"))
                        {
                            content.Experience2 = line.Replace("Experience_2:", "").Trim();
                        }
                        else if (line.Contains("Hobby:"))
                        {
                            content.Hobby = line.Replace("Hobby:", "").Trim();
                        }
                    }
                }
            }
            return content;
        }

        public void WriteNewPdfFile(PdfContent input)
        {
            string folderPath = "/Users/myav/Desktop/C# Console Apps/testItem";
            CreateFolderIfNotExists(folderPath);

            string fileName = input.Name;
            var uniqueFileName = GenerateUniqueFileName(fileName);
            var newFilePath = System.IO.Path.Combine(folderPath, uniqueFileName);

            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(newFilePath)))
            {
                Document document = new Document(pdfDocument);

                AddNameSection(document, input.Name);
                AddExperienceSection(document, input.Experience1, input.Experience2);
                if (input.Hobby != null)
                {
                    AddHobbySection(document, input.Hobby);
                }
                AddEducationSection(document, input.Education);

                document.Close();
            }
        }

        private void CreateFolderIfNotExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private string GenerateUniqueFileName(string fileName)
        {
            return Guid.NewGuid().ToString() + "_" + fileName + "_resume.pdf";
        }

        private void AddNameSection(Document document, string name)
        {
            document.Add(new Paragraph(name)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetFontSize(24)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(20));
        }

        private void AddExperienceSection(Document document, string experience1, string experience2)
        {
            document.Add(new AreaBreak());
            document.Add(new Paragraph("Experience:")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetMarginBottom(10));
            Table experienceTable = new Table(2)
                .UseAllAvailableWidth()
                .SetMarginBottom(20);
            experienceTable.AddCell(new Cell().Add(new Paragraph("Experience 1"))
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY));
            experienceTable.AddCell(new Cell().Add(new Paragraph(experience1)));
            if (experience2 != null)
            {
                experienceTable.AddCell(new Cell().Add(new Paragraph("Experience 2"))
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                experienceTable.AddCell(new Cell().Add(new Paragraph(experience2)));
            }
            document.Add(experienceTable);
        }

        private void AddHobbySection(Document document, string hobby)
        {
            document.Add(new AreaBreak());
            document.Add(new Paragraph("Hobby:")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetMarginBottom(10));
            document.Add(new Paragraph(hobby));
        }

        private void AddEducationSection(Document document, string education)
        {
            document.Add(new AreaBreak());
            document.Add(new Paragraph("Education:")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                .SetMarginBottom(10));
            document.Add(new Paragraph(education));
        }

        // public void WriteNewPdfFileBasedOnTemplate(PdfContent input)
        // {
        //     string outputFolder = "/Users/myav/Desktop/C# Console Apps/testItem";
        //     CreateFolderIfNotExists(outputFolder);

        //     string fileName = input.Name;
        //     var uniqueFileName = GenerateUniqueFileName(fileName);
        //     var outputPath = System.IO.Path.Combine(outputFolder, uniqueFileName);

        //     string inputPath = "/Users/myav/Desktop/Test-Items/template_resume_test.pdf";
        // }
    }
}

