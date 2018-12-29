INSERT INTO Diary
(
	UserId,
    Entry,
    Timestamp,
	Subject
)
VALUES
(
	@UserId,
    @Entry,
    @Timestamp,
	@Subject
)