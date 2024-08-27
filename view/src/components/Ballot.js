import ElectionDetails from "./ElectionDetails";

function Ballot({ballot, handleCandidateClick,  onVoteClick, showVoteButton = false}) {
    return (
        <div>
            <h1>{ballot.year}</h1>
            <div>
                {ballot.elections.map((election) => (
                    <ElectionDetails
                        key = {election.Id}
                        election = {election}
                        handleCandidateClick = {handleCandidateClick}
                        onVoteClick = {onVoteClick}
                        showVoteButton = {showVoteButton}  
                    />
                ))}
            </div>
        </div>
    );
}

export default Ballot;