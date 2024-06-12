function random_color(alpha=1.0)
{
    const r_c = () => Math.round(Math.random() * 255)
    return `rgba(${r_c()}, ${r_c()}, ${r_c()}, ${alpha}`
}

Chart.defaults.font.size = 16;

async function SQL_Graphs(){
    
    try {

        // Win Rate
        const WinRateResponse = await fetch(`http://localhost:5000/statistics/WinRate`, { method: 'GET' });
        if (WinRateResponse.ok) {
            let WinRateResults = await WinRateResponse.json();
            console.log('Win Rate:', WinRateResults);

            const WinRateData = WinRateResults.cards;
            const WinRateNames = WinRateData.map(e => e['Name']);
            const WinRateValues = WinRateData.map(e => e['Win_Rate']);
            const WinRateColors = WinRateData.map(() => random_color());

            const ctx_WinRate = document.getElementById('WinRateChart').getContext('2d');
            new Chart(ctx_WinRate, {
                type: 'bar',
                data: {
                    labels: WinRateNames,
                    datasets: [{
                        label: 'Win Rate (%)',
                        backgroundColor: WinRateColors,
                        borderWidth: 2,
                        data: WinRateValues
                    }]
                }
            });
        }


        // Matches Played
        const MatchesPlayedResponse = await fetch(`http://localhost:5000/statistics/MatchesPlayed`, { method: 'GET' });
        if (MatchesPlayedResponse.ok) {
            let MatchesPlayedResults = await MatchesPlayedResponse.json();
            console.log('Matches Played:', MatchesPlayedResults);

            const MatchesPlayedData = MatchesPlayedResults.cards;
            const MatchesPlayedNames = MatchesPlayedData.map(e => e['Name']);
            const MatchesPlayedValues = MatchesPlayedData.map(e => e['Matches_Played']);
            const MatchesPlayedColors = MatchesPlayedData.map(() => random_color());

            const ctx_MatchesPlayed = document.getElementById('MatchesPlayedChart').getContext('2d');
            new Chart(ctx_MatchesPlayed, {
                type: 'bar',
                data: {
                    labels: MatchesPlayedNames,
                    datasets: [{
                        label: 'Matches Played',
                        backgroundColor: MatchesPlayedColors,
                        borderWidth: 2,
                        data: MatchesPlayedValues
                    }]
                }
            });
        }


        // Top Cards
        const TopCardsResponse = await fetch(`http://localhost:5000/statistics/TopCards`, { method: 'GET' });
        if (TopCardsResponse.ok) {
            let TopCardsResults = await TopCardsResponse.json();
            console.log('TopCards:', TopCardsResults);

            const TopCardsData = TopCardsResults.cards;
            const TopCardsNames = TopCardsData.map(e => e['Card_Name']);
            const TopCardsValues = TopCardsData.map(e => e['Deck_Count']);
            const TopCardsColors = TopCardsData.map(() => random_color());

            const ctx_TopCards = document.getElementById('TopCardsChart').getContext('2d');
            new Chart(ctx_TopCards, {
                type: 'pie',
                data: {
                    labels: TopCardsNames,
                    datasets: [{
                        label: 'Times Used',
                        backgroundColor: TopCardsColors,
                        borderWidth: 2,
                        data: TopCardsValues
                    }]
                }
            });
        }

        // Top 3 Players
        const Top3Response = await fetch(`http://localhost:5000/statistics/Top3Players`, { method: 'GET' });
        if (Top3Response.ok) {
            let Top3Results = await Top3Response.json();
            console.log('Top 3 Players:', Top3Results);

            const Top3Data = Top3Results.cards;
            const Top3Names = Top3Data.map(e => e['Player_Name']);
            const Top3Values = Top3Data.map(e => e['Matches_Won']);
            const Top3Colors = Top3Data.map(() => random_color());

            const ctx_Top3 = document.getElementById('Top3Chart').getContext('2d');
            new Chart(ctx_Top3, {
                type: 'bar',
                data: {
                    labels: Top3Names,
                    datasets: [{
                        label: 'Top 3 Players',
                        backgroundColor: Top3Colors,
                        borderWidth: 2,
                        data: Top3Values
                    }]
                }
            });
        }
    
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

SQL_Graphs();