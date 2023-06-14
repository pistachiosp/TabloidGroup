SELECT *  
FROM BlogTag
JOIN Blog ON Blog.Id = BlogTag.BlogId
Join Tag ON BlogTag.TagId = Tag.Id
where blog.id = BlogTag.BlogId


SELECT Id, Title, URL
FROM Blog