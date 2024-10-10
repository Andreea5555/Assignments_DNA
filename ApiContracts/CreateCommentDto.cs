namespace DTOs;

public class CreateCommentDto
{
    public string Body { get; set; }
    public int PostID { get; set; }
    public int UserID { get; set; }
}