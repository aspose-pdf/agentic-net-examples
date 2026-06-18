using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Comparison;

namespace PdfComparisonExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // In a real web application the outputStream would be HttpResponse.Body.
            // For this console demo we write the result to a file named diff.pdf.
            using (FileStream fileStream = new FileStream("diff.pdf", FileMode.Create, FileAccess.Write))
            {
                WriteComparisonResult(fileStream);
            }
        }

        static void WriteComparisonResult(Stream outputStream)
        {
            // ---------- Create first sample PDF ----------
            using (Document doc1 = new Document())
            {
                Page page1 = doc1.Pages.Add();
                page1.Paragraphs.Add(new TextFragment("Hello World!"));
                doc1.Save("sample1.pdf");

                using (Document sourceDoc1 = new Document("sample1.pdf"))
                {
                    // ---------- Create second sample PDF ----------
                    using (Document doc2 = new Document())
                    {
                        Page page2 = doc2.Pages.Add();
                        page2.Paragraphs.Add(new TextFragment("Hello Aspose PDF!"));
                        doc2.Save("sample2.pdf");

                        using (Document sourceDoc2 = new Document("sample2.pdf"))
                        {
                            // ---------- Perform graphical comparison ----------
                            Page srcPage1 = sourceDoc1.Pages[1]; // 1‑based indexing
                            Page srcPage2 = sourceDoc2.Pages[1];

                            using (Document resultDoc = new Document())
                            {
                                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                                comparer.ComparePagesToPdf(srcPage1, srcPage2, resultDoc);

                                // Write the comparison result directly to the provided stream.
                                resultDoc.Save(outputStream);
                            }
                        }
                    }
                }
            }
        }
    }
}