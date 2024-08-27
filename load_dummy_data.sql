-- Users
INSERT INTO Users (Email, Password, FirstName, LastName, DateOfBirth, IsVerified, Affiliation)
VALUES 
('john.doe@example.com', 'Password123', 'John', 'Doe', '1985-05-15', 1, 'Independent'),
('jane.smith@example.com', 'P@ssw0rd', 'Jane', 'Smith', '1978-09-25', 1, 'Republican'),
('michael.johnson@example.com', 'Secret123', 'Michael', 'Johnson', '1990-03-10', 0, 'Democrat'),
('amy.brown@example.com', 'Brownie456', 'Amy', 'Brown', '1989-12-08', 1, 'Republican'),
('david.wong@example.com', 'Wonger789', 'David', 'Wong', '1973-07-22', 1, 'Democrat');

-- Ballots
INSERT INTO Ballots (StartDate, EndDate) 
VALUES 
('2022-01-01', '2022-01-15'), -- Past
('2023-07-01', '2023-07-15'), -- Past
('2024-03-01', '2024-06-01'), -- Current (Starts in March, Ends in June)
('2027-01-01', '2027-01-15'), -- Future
('2028-07-01', '2028-07-15'); -- Future

-- Candidates
INSERT INTO Candidates (Age, Description, Policy1, Policy2, FirstName, LastName, Party, IsIncumbent) 
VALUES 
(55, 'Experienced economist with a focus on tax reform', 'Tax reduction for middle class', 'Infrastructure development', 'Don', 'Apple', 'Republican', 0),
(48, 'Advocate for healthcare reform and improved access to education', 'Universal healthcare coverage', 'Education funding', 'Michael', 'Johnson', 'Democrat', 1),
(62, 'Former mayor with a track record of successful urban development projects', 'Infrastructure improvement', 'Economic revitalization', 'James', 'Williams', 'Independent', 0),
(40, 'Business owner and entrepreneur committed to job creation', 'Small business support', 'Job training programs', 'Guy', 'Taco', 'Republican', 0),
(42, 'Environmental activist focused on sustainability and renewable energy', 'Climate change mitigation', 'Green energy initiatives', 'Emma', 'Davis', 'Green', 0),
(38, 'Technology entrepreneur advocating for innovation and digital literacy', 'Tech education programs', 'Start-up funding', 'Alex', 'Garcia', 'Independent', 0),
(50, 'Experienced politician with a background in law enforcement', 'Law and order', 'Community policing initiatives', 'Robert', 'Martinez', 'Republican', 0),
(47, 'Educator and community organizer passionate about education equality', 'Education reform', 'Youth empowerment programs', 'Laura', 'Robinson', 'Democrat', 1),
(55, 'Business leader with a focus on economic growth and job creation', 'Economic stimulus packages', 'Trade promotion initiatives', 'Daniel', 'Taylor', 'Republican', 0),
(58, 'Public servant committed to social justice and civil rights', 'Social equality initiatives', 'Criminal justice reform', 'Mark', 'Gates', 'Democrat', 0);

-- Elections
INSERT INTO Elections (BallotId, Candidate1Id, Candidate2Id, Position, Candidate1VoteCount, Candidate2VoteCount)
VALUES 
(1, 1, 2, 'Mayor', 1234, 1456),
(2, 5, 6, 'Senator', 3872, 4123),
(2, 7, 8, 'Governor', 5423, 5001),
(3, 9, 10, 'President', 12000, 15000),
(3, 3, 4, 'Vice President', 8000, 9000),
(4, 5, 7, 'Senator', 4000, 4500),
(4, 6, 8, 'Governor', 3500, 3800),
(5, 9, 10, 'President', 500, 600),
(5, 1, 6, 'Vice President', 300, 400);

-- Votes
INSERT INTO Votes (ElectionId, CandidateId, UserId) 
VALUES 
(1, 1, 1),
(1, 2, 2),
(2, 3, 3),
(2, 4, 4);