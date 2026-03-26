using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using System.Drawing.Imaging;

public class Program
{
    public static void Main()
    {
        string firstPdfPath = "first.pdf";
        string secondPdfPath = "second.pdf";
        string imagesDirectory = "diff_images";
        string zipFileName = "diff_images.zip";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine("File not found: " + firstPdfPath);
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("File not found: " + secondPdfPath);
            return;
        }

        Directory.CreateDirectory(imagesDirectory);

        using (Document document1 = new Document(firstPdfPath))
        using (Document document2 = new Document(secondPdfPath))
        {
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            comparer.CompareDocumentsToImages(document1, document2, imagesDirectory, "diff_", ImageFormat.Png);
        }

        using (FileStream zipToOpen = new FileStream(zipFileName, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
        {
            string[] imageFiles = Directory.GetFiles(imagesDirectory);
            foreach (string imagePath in imageFiles)
            {
                string entryName = Path.GetFileName(imagePath);
                archive.CreateEntryFromFile(imagePath, entryName);
            }
        }

        Console.WriteLine("Comparison images zipped to " + zipFileName);
    }
}