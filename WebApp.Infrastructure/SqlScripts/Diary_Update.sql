UPDATE Diary SET
	Subject = @Subject,
	Timestamp = @Timestamp,
	Entry = @Entry
WHERE UserId = @UserId AND Id = @Id