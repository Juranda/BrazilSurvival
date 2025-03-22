import { zodResolver } from '@hookform/resolvers/zod';
import { FormEvent, useState } from 'react';
import { Controller, useFieldArray, useForm } from 'react-hook-form';
import { z } from 'zod';


const challengeSchema = z.object({
    title: z.string().max(255),
    options: z.array(z.object({
        action: z.string().min(10).max(100),
        consequences: z.array(z.object({
            answer: z.string().min(10).max(100),
            consequence: z.string().min(10).max(100),
            health: z.number().min(0).optional(),
            money: z.number().min(0).optional(),
            power: z.number().min(0).optional(),
        }))
    })).min(4)
});

type FormType = z.infer<typeof challengeSchema>;

function Challenges() {
    const {
        control,
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<FormType>({
        resolver: zodResolver(challengeSchema),
        defaultValues: {
            title: "",
            options: [
                {
                    action: "",
                    consequences: [
                        {
                            answer: "",
                            consequence: "",
                            health: 0,
                            money: 0,
                            power: 0
                        }
                    ]
                }
            ]
        }
    });

    const {
        fields: options,
        update:
        updateOption,
        remove
    } = useFieldArray({
        control,
        name: "options",
    });

    function onSubmit(e: FormType) {
        console.log(e);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <div>
                <label>Título do Desafio:</label>
                <input {...register("title")} />
                {errors.title && <p>{errors.title.message}</p>}
            </div>

            {options.map((option, index) => (
                <div key={option.id} style={{ border: "1px solid #ccc", padding: "10px", marginTop: "10px" }}>
                    <label>Opção {index + 1}:</label>
                    <input {...register(`options.${index}.action`)} />
                    {errors.options?.[index]?.action && <p>{errors.options[index].action.message}</p>}

                    <div>
                        <label>Consequências:</label>
                        <ul>
                            {option.consequences.map((_, cIndex) => (
                                <>
                                    <li key={cIndex}>
                                        <Controller
                                            control={control}
                                            name={`options.${index}.consequences.${cIndex}`}
                                            render={({ field }) => (
                                                <>
                                                    <input {...field} value={field.value.answer} placeholder="Answer" />
                                                    <input {...field} value={field.value.consequence} placeholder="Consequence" />
                                                    <input {...field} value={field.value.health} placeholder="Health" type="number" />
                                                    <input {...field} value={field.value.money} placeholder="Money" type="number" />
                                                    <input {...field} value={field.value.power} placeholder="Power" type="number" />
                                                </>
                                            )} />
                                    </li>
                                    {
                                        options.length > 1 && <button onClick={() => remove(index)}>X</button>
                                    }
                                </>
                            ))}
                        </ul>
                        {errors.options?.[index]?.consequences && (
                            <p>{errors.options[index].consequences.message}</p>
                        )}

                        {option.consequences.length < 5 && (
                            <button
                                type="button"
                                onClick={() =>
                                    updateOption(index, {
                                        ...option,
                                        consequences: [...option.consequences, {
                                            answer: "",
                                            consequence: "",
                                            health: 0,
                                            money: 0,
                                            power: 0
                                        }],
                                    })
                                }
                            >
                                Adicionar Consequência
                            </button>
                        )}
                    </div>
                </div>
            ))}

            <button type="submit">Enviar</button>
        </form>
    );
};

export default Challenges;