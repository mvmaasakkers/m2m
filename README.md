
Setup:

`docker-compose up -d`
`dotnet ef database update`


This prefills the database

`GET https://localhost:5001/categories/fill`

This gives an error

`GET https://localhost:5001/categories`

This works but is a lot of manual work to set up.
This is the result I am looking for. 
Ideally without the postTags: null in the body but that could be done 
with the `[JsonIgnore]` attribute.
 
`GET https://localhost:5001/categories/select`