import Challenge from "./game/types/Challenge";

class StaticDatabase {
    private static challenges: Challenge[] = [
        {
            title: "Você acabou de chegar na rodoviária após um longo dia de trabalho, mas percebe que o ônibus está prestes a sair.",
            options: [
                {
                    action: "Pegar outro ônibus",
                    answer: "Você espera para pegar outro ônibus",
                    consequence: "Dois caras aparecem numa moto e te assaltam",
                    money: -6, 
                    power: -2
                },
                {
                    action: "Pegar outro ônibus que está lotado",
                    answer: "Por sorte você encontra um outro ônibus que vai para o mesmo lugar",
                    consequence: "Você consegue chegar em casa mas percebe que sua carteira foi roubada",
                    health: 2, 
                    money: -3
                },
                {
                    action: "Tentar alcançar o ônibus",
                    answer: "Você corre até suas pernas não aguentarem mais",
                    consequence: "Além de perder o ônibus agora você está muito cansado",
                    health: -2
                },
                {
                    action: "Pedir um uber",
                    answer: "Você tenta chamar um uber no meio da noite",
                    consequence: "Você consegue voltar para casa em segurança",
                    money: -3
                }
            ]
        },
        {
            title: "Uma pessoa que você nunca viu na sua vida para na sua frente e faz a seguinte pergunta: É biscoito ou bolacha?",
            options: [
                {
                    action: "Biscoito",
                    answer: "Você responde biscoito",
                    consequence: "A pessoa não concorda com você e vai embora",
                    power: -3
                },
                {
                    action: "Bolacha",
                    answer: "Você responde bolacha",
                    consequence: "A pessoa não concorda com você e vai embora",
                    power: -3
                },
                {
                    action: "Não deviamos nos preocupar com isso",
                    answer: "Você tenta convece-la de que este não é um tópico importante",
                    consequence: "Após uma longa conversa sobre isso ela desiste do assunto e vocês se tornam bons amigos",
                    power: 5, 
                    money: 2
                },
                {
                    action: "Começar uma conversa sobre futebol, religião e política",
                    answer: "Por algum motivo você decide conversar sobre tópicos muito sensíveis.",
                    consequence: "Inacreditavelmente, essa pessoa concorda com tudo que você disse. Ela diz que trabalha no governo e te oferece um cargo.",
                    health: 2, 
                    power: 6, 
                    money: 5
                }
            ]
        }
    ];

    static async getRandomChallenge(): Promise<Challenge> {
        return this.challenges[Math.floor(Math.random() * this.challenges.length)];
    }

    static async getRandomChallenges(quantity: number = 10): Promise<Challenge[]> {
        return this.challenges.slice(0, quantity);
    }
}

export default StaticDatabase;