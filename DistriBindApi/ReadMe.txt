I kept it all in one project, in one solution, to keep it simple.
Ideally in a larger project it would be separated into multiple projects. Logic would also go into
services layer/project.

Basic indexing in AppDbContext.cs. I used EF Core as ORM.

This is a basic example of POST object for the /api/expenses endpoint.
We pass UserId as for the purpose of this exercise I didn't add auth (not enough time).
In the ideal world we would have a JWT token and we would pass the userId from the token.

{
  "amount": 100.50,
  "userId": 1,
  "description": "supermarket shopping"
}


To get expense by ID: HTTP GET: /api/expenses/{id}
To get expense by ID: HTTP DELETE: /api/expenses/{id}

The controller method uses strategy pattern to fish for words in "Description" to dynamically
assign the category Enum. This is rudimentary but it works for the purpose of this exercise.

The controller method is also responsible for basic validating the model. If the model is not valid,
it returns a 400 Bad Request response with the validation errors. Ideally I'd use fluid validation.


SQL query to retrieve monthly Expenses for user:

SELECT
    DATEPART(YEAR, CreatedOn) AS Year,
    DATEPART(MONTH, CreatedOn) AS Month,
    SUM(Amount) AS TotalExpense
FROM
    Expenses
WHERE
    UserId = @UserId
GROUP BY
    DATEPART(YEAR, CreatedOn), DATEPART(MONTH, CreatedOn)
ORDER BY
    Year, Month;
    
    
Make sure to swap @UserId with the actual userId you want to filter by.

There is also an endpoint for this:

http://localhost:5062/api/expenses/monthly-expenses/1


I ran out of time to write Unit Tests.