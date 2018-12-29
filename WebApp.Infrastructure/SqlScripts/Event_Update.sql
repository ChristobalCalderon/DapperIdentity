UPDATE Event SET
	EventType = @EventType,
	Describition = @Describition,
	Date = @Date,
	Location = @Location
WHERE Id = @Id