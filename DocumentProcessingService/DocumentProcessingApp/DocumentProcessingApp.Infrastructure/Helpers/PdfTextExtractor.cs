using System.Text;

namespace DocumentProcessingApp.Infrastructure.Helpers
{
    public static class PdfTextExtractorHelper
    {
        public static string ExtractText(string filePath)
        {
            using var reader = new iTextSharp.text.pdf.PdfReader(filePath);
            var sb = new StringBuilder();

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                sb.Append(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i));
            }

            return sb.ToString();
        }
    }
}